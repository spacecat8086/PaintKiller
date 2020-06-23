using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace PaintKiller
{
    public class Form1 : Form
    {
        public static Type[] shapeTypes { get; private set; }
        public static Dictionary<Type, ConstructorInfo> shapeCreators { get; private set; }
        public Scene scene;
        private Scene UI;
        private event PaintEventHandler UIPaint;
        private Type selectedShape
        {
            get
            {
                if (selectedIndex > 0)
                {
                    return shapeTypes[selectedIndex - 1];
                }
                return null;
            }
        }
        private int selectedIndex;
        private int hoverIndex;
        private Point mouseStart;
        private bool isDrawing;
        public Pen currentPen;
        public SolidBrush currentBrush;
        private OptionForm fmOptions;
        private void InitShapes() 
        {
            shapeCreators = new Dictionary<Type, ConstructorInfo>();
            shapeTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof (Shape)).ToArray();

            string pluginPath = Directory.GetCurrentDirectory() + "\\Plugins";
            var plugins = new string[0];
            try
            {
                plugins = Directory.GetFiles(pluginPath, "*.dll");
            }
            catch
            {
                
            }

            foreach (string path in plugins)
            {
                try
                {
                    var asm = Assembly.LoadFrom(path);
                    Type[] newShapes = asm.GetTypes().Where(t => t.BaseType == typeof (Shape)).ToArray();
                    shapeTypes = shapeTypes.Concat(newShapes).ToArray();                    
                }
                catch
                {
                    throw(new Exception($"Can't load module {path}!"));
                }            
            }

            foreach (var shapeType in shapeTypes)
            {
                shapeCreators.Add(shapeType, shapeType.GetConstructors().First());
            }
        }
        private void DrawSomething()
        {
            Point a = new Point(100, 150);
            Point b = new Point(200, 250);

            foreach (var shapeType in shapeTypes)
            {
                var shapeParams = new object[4] { Pens.Black, Brushes.Coral, a, b };
                scene.Add(shapeCreators[shapeType].Invoke(shapeParams) as Shape);

                a.X += 150;
                b.X += 150;
            }            
        }
        private void Redraw(object Sender, PaintEventArgs e)
        {
            scene.Draw(e); 
            UIPaint.Invoke(Sender, e);           
        }
        private void InitUI()
        {
            UI = new Scene();

            Point a = new Point(25, 125);
            Point b = new Point(75, 175);

            foreach (var shapeType in shapeTypes)
            {
                var shapeParams = new object[4] { new Pen(Color.DarkSlateGray, 4), Brushes.Transparent , a, b };
                UI.Add(shapeCreators[shapeType].Invoke(shapeParams) as Shape);

                a.Y += 75;
                b.Y += 75;
            }

            selectedIndex = -1;
            hoverIndex = -1;
        }
        private void DrawUI(object Sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            // UI bar
            graphics.FillRectangle(Brushes.LightSteelBlue, 0, 0, 100, Height);
            // Selection box
            if (selectedIndex > -1)
            {
                graphics.FillRectangle(Brushes.Gainsboro, 12, 37 + selectedIndex * 75, 75, 75);
            }
            // Hover box
            if (hoverIndex > -1)
            {
                graphics.FillRectangle(Brushes.LightBlue, 12, 37 + hoverIndex * 75, 75, 75);
            }
            // Tool icon
            Point top = new Point(53, 39);
            Point left = new Point(43, 54);
            Point bottom = new Point(73, 74);
            Point right = new Point(83, 59);

            graphics.DrawLine(new Pen(Color.DarkSlateGray, 4), 30, 105, 70, 45);
            graphics.FillPolygon(Brushes.DarkSlateGray, new Point[4] { top, left, bottom, right } );

            // Shape buttons
            UI.Draw(e);             
        }
        private void MouseMoveHandler(object Sender, MouseEventArgs e)
        {
            int index;
            if (e.X < 100)
            {
                index = (e.Y + 38) / 75 - 1;
                if (index < 0 || index > shapeTypes.Length)
                {
                    index = -1;
                }
            }
            else
            {
                index = -1;
            }
            
            if (hoverIndex != index)
            {
                hoverIndex = index;
                Invalidate(new System.Drawing.Rectangle(0, 0, 100, Height));
            }
        }
        private void ClickHandler(object Sender, MouseEventArgs e)
        {
            if (e.X < 100)                      // GUI area
            {
                int index = (e.Y + 38) / 75 - 1;
                
                if (index == 0)
                {
                    fmOptions = new OptionForm();  
                    fmOptions.Show(this);
                }
                else if (index > 0 && index <= shapeTypes.Length)
                {
                    selectedIndex = index;                    
                }
                else
                {
                    selectedIndex = -1;
                }
                Invalidate(new System.Drawing.Rectangle(0, 0, 100, Height));
            }                           
            else if (selectedShape != null)     // Editor Area
            {
                if (isDrawing)
                {
                    isDrawing = false;          // Finish drawing the shape

                    Pen pen = currentPen.Clone() as Pen;
                    Brush brush = currentBrush.Clone() as Brush;

                    var shapeParams = new object[4] { pen, brush, mouseStart, e.Location };
                    scene.Add(shapeCreators[selectedShape].Invoke(shapeParams) as Shape);
                    Invalidate(new System.Drawing.Rectangle(100, 0, Width, Height));
                }
                else
                {                               // Start drawing the shape
                    isDrawing = true;
                    mouseStart = e.Location;
                }
            }
        }
        public Form1()
        {            
            // IsMdiContainer = true;
            DoubleBuffered = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            BackColor = Color.White;
            Text = "PaintKiller";

            currentPen = new Pen(Color.Black, 4);
            currentBrush = new SolidBrush(Color.DodgerBlue);

            InitShapes();
            InitUI();
            scene = new Scene();
            
            Paint += new PaintEventHandler(Redraw);
            UIPaint += new PaintEventHandler(DrawUI);
            MouseMove += new MouseEventHandler(MouseMoveHandler);
            MouseClick += new MouseEventHandler(ClickHandler);      
        }
    }
}
