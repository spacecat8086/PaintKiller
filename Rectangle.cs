using System.Windows.Forms;
using System.Drawing;

class Rectangle : Shape
{
    public Rectangle(int x, int y, int width, int height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }
    public override void Draw(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        graphics.DrawRectangle(new Pen(Color.Crimson, 4), this.X, this.Y, this.Width, this.Height);
    }
    private int X, Y, Width, Height;
}