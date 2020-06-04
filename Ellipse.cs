using System.Windows.Forms;
using System.Drawing;

public class Ellipse : Shape
{
    private int x, y, width, height;
    public Ellipse(Pen pen, Brush brush, Point a, Point b) : base(pen, brush, a, b)
    {
        x = a.X;
        y = a.Y;
        width = b.X - a.X;
        height = b.Y - a.Y;
    }
    public override void Draw(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        
        graphics.FillEllipse(fill, x, y, width, height);
        if (outline.Width > 0.01 )
        {
            graphics.DrawEllipse(outline, x, y, width, height);
        }
    }
}