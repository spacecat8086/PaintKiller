using System.Windows.Forms;
using System.Drawing;

public class Triangle : Shape
{
    private Point[] points;
    public Triangle(Pen pen, Brush brush, Point a, Point b) : base(pen, brush, a, b)
    {
        points = new Point[3];
        points[0].X = a.X;
        points[0].Y = b.Y;
        points[1].X = (a.X + b.X) / 2;
        points[1].Y = a.Y;
        points[2].X = b.X;
        points[2].Y = b.Y;

    }
    public override void Draw(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        
        graphics.FillPolygon(fill, points);
        if (outline.Width > 0.01)
        {
            graphics.DrawPolygon(outline, points);
        }
    }
}