using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

public class Scene
{
    private Pen currentOutline { get; set; }
    private Brush currentBrush { get; set; }
    private List<Shape> shapes;
    public Scene()
    {
        currentOutline = new Pen(Color.Black, 4);
        currentBrush = new SolidBrush(Color.DodgerBlue);

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