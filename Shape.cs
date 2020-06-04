using System.Windows.Forms;
using System.Drawing;

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
    public string Serialize()
    {
        var s = new string(string.Empty);

        s = GetType() + " " + outline.Width + outline.Color.ToString() + fill.Color + 
        edges[0].X + " " + edges[0].Y + " " + edges[1].X + " " + edges[0].Y;


        return s;
    }
}