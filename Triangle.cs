using System.Windows.Forms;
using System.Drawing;

class Triangle : Shape
{
    public Triangle(int ax, int ay, int bx, int by, int cx, int cy)
    {
        this.Points = new Point[3];
        this.Points[0].X = ax;
        this.Points[0].Y = ay;
        this.Points[1].X = bx;
        this.Points[1].Y = by;
        this.Points[2].X = cx;
        this.Points[2].Y = cy;

    }
    public override void Draw(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        graphics.DrawPolygon(new Pen(Color.Crimson, 4), this.Points);
    }
    private Point[] Points;
}