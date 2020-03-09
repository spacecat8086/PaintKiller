using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintKiller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.scene = new Scene();
            InitShapes();
        }
        private void InitShapes()
        {
            this.scene.Add(new Ellipse(100, 150, 100, 100));
            this.scene.Add(new Rectangle(250, 150, 100, 100));
            this.scene.Add(new Triangle(400, 150, 500, 150, 450, 250));
            this.scene.Add(new Line(550, 150, 650, 250));
            this.scene.Add(new Line(550, 250, 650, 150));
        }
        private void Redraw(object Sender, PaintEventArgs e)
        {
            this.scene.Draw(e);            
        }
        private Scene scene;
    }
}
