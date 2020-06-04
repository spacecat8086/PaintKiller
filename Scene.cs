using System.Collections.Generic;
using System.Windows.Forms;

public class Scene
{
    public List<Shape> shapes { get; set; }
    public Scene()
    {
        shapes = new List<Shape>();
    }
    public void Add(Shape shape)
    {
        shapes.Add(shape);
    }
    public void Remove(Shape shape)
    {
        shapes.Remove(shape);
    }
    public void Draw(PaintEventArgs e)
    {
        foreach (Shape shape in shapes)
        {
            shape.Draw(e);
        }
    }
}