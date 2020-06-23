using System;
using System.Windows.Forms;
using System.Drawing;
using PaintKiller;

public abstract class Shape
{
    protected SolidBrush fill;
    protected Pen outline;
    protected Point[] edges;
    public Shape(Pen pen, SolidBrush brush, Point a, Point b)
    {
        outline = pen;
        fill = brush;
        edges = new Point[2] { a, b };
    }
    public abstract void Draw(PaintEventArgs e);
    public Shape GetObject { get => this; }
    public string Serialize()
    {
        string outlineColor = outline.Color.A + " " + outline.Color.R + " " + outline.Color.G + " " + outline.Color.B;
        string fillColor = fill.Color.A + " " + fill.Color.R + " " + fill.Color.G + " " + fill.Color.B;
        
        var s = new string(string.Empty);

        s = GetType() + " " + outline.Width + " " + outlineColor + " " + fillColor + 
        " " + edges[0].X + " " + edges[0].Y + " " + edges[1].X + " " + edges[1].Y;

        return s;
    }
    public static Shape Deserialize(string s)
    {
        string[] values;
        Type shape = null;

        values = s.Split(' ', 14);

        // Get object shape
        if (values[0] != null)
        {
            foreach(var shapeType in Form1.shapeTypes)
            {
                if (values[0].Equals(shapeType.Name))
                {
                    shape = shapeType;
                    break;
                }
            }
            if (shape == null)
            {
                MessageBox.Show($"Unknown shape '{values[0]}'. This object will be skipped", "Error");
                return null;
            }
        }
        else
        {
            MessageBox.Show("Can't create object from empty string", "Error");
            return null;            
        }

        // Get object outline width and color components
        int W, A, R, G, B;

        if (values[1] != null)
            int.TryParse(values[1], out W);
        else
            W = 4;

        if (values[2] != null)
            int.TryParse(values[2], out A);
        else
            A = 255;

        if (values[3] != null)
            int.TryParse(values[3], out R);
        else
            R = 0;

        if (values[4] != null)
            int.TryParse(values[4], out G);
        else
            G = 0;

        if (values[5] != null)
            int.TryParse(values[5], out B);
        else
            B = 0;

        var pen = new Pen(Color.FromArgb(A, R, G, B), W);
        
        // Get object filling color components
        if (values[6] != null)
            int.TryParse(values[6], out A);
        else
            A = 255;

        if (values[7] != null)
            int.TryParse(values[7], out R);
        else
            R = 0;

        if (values[8] != null)
            int.TryParse(values[8], out G);
        else
            G = 0;

        if (values[9] != null)
            int.TryParse(values[9], out B);
        else
            B = 0;

        var brush = new SolidBrush(Color.FromArgb(A, R, G, B));

        // Get object top-left and bottom-rigth positions
        int x1, y1, x2, y2;

        if (values[10] != null)
            int.TryParse(values[10], out x1);
        else
            x1 = 0;

        if (values[11] != null)
            int.TryParse(values[11], out y1);
        else
            y1 = 0;

        if (values[12] != null)
            int.TryParse(values[12], out x2);
        else
            x2 = 20;

        if (values[13] != null)
            int.TryParse(values[13], out y2);
        else
            y2 = 20;

        var pointA = new Point(x1, y1);
        var pointB = new Point(x2, y2);

        // Get and invoke object constructor
        var shapeParams = new object[4] { pen, brush, pointA, pointB };
        return Form1.shapeCreators[shape].Invoke(shapeParams) as Shape;
    }
}