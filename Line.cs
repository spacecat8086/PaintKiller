using System.Windows.Forms;
using System.Drawing;

public class Line : Shape
{
    private int x0, y0, x1, y1;
    public Line(Pen pen, Brush brush, Point a, Point b) : base(pen, brush, a, b)
    {
        x0 = a.X;
        y0 = a.Y;
        x1 = b.X;
        y1 = b.Y;
    }
    public override void Draw(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        
        if (outline != null)
        {
            graphics.DrawLine(outline, x0, y0, x1, y1);
        }
    }
}