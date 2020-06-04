using System.Windows.Forms;
using System.Drawing;

public class Rectangle : Shape
{
    private int x, y, width, height;
    public Rectangle(Pen pen, Brush brush, Point a, Point b) : base(pen, brush, a, b)
    {
        x = a.X;
        y = a.Y;
        width = b.X - a.X;
        if (width < 0)
        {
            x = b.X;
            width = -width;            
        }
        height = b.Y - a.Y;
        if (height < 0)
        {
            y = b.Y;
            height = -height;
        }
    }
    public override void Draw(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;

        graphics.FillRectangle(fill, x, y, width, height); 
        if (outline.Width > 0.01)
        {
            graphics.DrawRectangle(outline, x, y, width, height);
        }
    }
}