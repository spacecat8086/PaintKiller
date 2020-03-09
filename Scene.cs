using System.Collections.Generic;
using System.Windows.Forms;

class Scene
{
    public Scene()
    {
        this.Shapes = new List<Shape>();
    }
    private List<Shape> Shapes;
    public void Add(Shape shape)
    {
        Shapes.Add(shape);
    }
    public void Draw(PaintEventArgs e)
    {
        foreach (Shape shape in this.Shapes)
        {
            shape.Draw(e);
        }
    }
}