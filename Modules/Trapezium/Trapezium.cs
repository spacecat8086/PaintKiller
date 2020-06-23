using System.Windows.Forms;
using System.Drawing;

public class Trapezium : Shape
{
    private Point[] points;
    public Trapezium(Pen pen, SolidBrush brush, Point a, Point b) : base(pen, brush, a, b)
    {
        points = new Point[4];
        points[0].X = a.X;
        points[0].Y = b.Y;
        points[1].X = (a.X * 3 + b.X) / 4;
        points[1].Y = a.Y;
        points[2].X = (a.X + b.X * 3) / 4;
        points[2].Y = a.Y;
        points[3].X = b.X;
        points[3].Y = b.Y;

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