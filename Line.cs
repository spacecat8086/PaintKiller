using System.Windows.Forms;
using System.Drawing;

class Line : Shape
{
    public Line(int x0, int y0, int x1, int y1)
    {
        this.X0 = x0;
        this.Y0 = y0;
        this.X1 = x1;
        this.Y1 = y1;
    }
    public override void Draw(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        graphics.DrawLine(new Pen(Color.Crimson, 4), this.X0, this.Y0, this.X1, this.Y1);
    }
    private int X0, Y0, X1, Y1;
}