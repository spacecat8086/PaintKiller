using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace PaintKiller
{
    public class Form1 : Form
    {
        private Type[] shapeTypes;
        private Dictionary<Type, ConstructorInfo> shapeCreators;
        private Scene scene;
        private Scene UI;
        private event PaintEventHandler UIPaint;
        private Type selectedShape;
        private int selectedIndex;
        private int hoverIndex;
        private Point mouseStart;
        private bool isDrawing;
        private Pen currentPen { get; set; }
        private Brush currentBrush { get; set; }
        private void InitShapes() 
        {
            shapeCreators = new Dictionary<Type, ConstructorInfo>();
            shapeTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof (Shape)).ToArray();

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

            Point a = new Point(25, 75);
            Point b = new Point(75, 125);

            foreach (var shapeType in shapeTypes)
            {
                var shapeParams = new object[4] { new Pen(Color.DarkSlateGray, 4), null, a, b };
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
                graphics.FillRectangle(Brushes.Gainsboro, 12, 62 + selectedIndex * 75, 75, 75);
            }
            // Hover box
            if (hoverIndex > -1)
            {
                graphics.FillRectangle(Brushes.LightBlue, 12, 62 + hoverIndex * 75, 75, 75);
            }
            // Shape buttons
            UI.Draw(e);             
        }
        private void MouseMoveHandler(object Sender, MouseEventArgs e)
        {
            int index;
            if (e.X < 100)
            {
                index = (e.Y + 13) / 75 - 1;
                if (index < 0 || index >= shapeTypes.Length)
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
                int index = (e.Y + 13) / 75 - 1;
                if (index >= 0 && index < shapeTypes.Length)
                {
                    selectedShape = shapeTypes[index];
                    selectedIndex = index;
                }
                else
                {
                    selectedShape = null;
                    selectedIndex = -1;
                }
                Invalidate(new System.Drawing.Rectangle(0, 0, 100, Height));
            }                           
            else if (selectedShape != null)     // Editor Area
            {
                if (isDrawing)
                {
                    isDrawing = false;          // Finish drawing the shape

                    var shapeParams = new object[4] { currentPen, currentBrush, mouseStart, e.Location };
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
            DoubleBuffered = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            BackColor = Color.White;
            Text = "PaintKiller";
            
            InitShapes();
            InitUI();
            scene = new Scene();

            currentPen = new Pen(Color.Black, 4);
            currentBrush = new SolidBrush(Color.DodgerBlue);
            
            Paint += new PaintEventHandler(Redraw);
            UIPaint += new PaintEventHandler(DrawUI);
            MouseMove += new MouseEventHandler(MouseMoveHandler);
            MouseClick += new MouseEventHandler(ClickHandler);
        }
    }
}
