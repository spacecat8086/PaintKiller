using System.Windows.Forms;
using System.Drawing;

public abstract class Shape
{
    protected Brush fill;
    protected Pen outline;
    public Shape(Pen pen, Brush brush, Point a, Point b)
    {
        outline = pen;
        fill = brush;
    }
    public abstract void Draw(PaintEventArgs e);
}