using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

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
    public void Empty()
    {
        shapes.Clear();
    }
    public void Draw(PaintEventArgs e)
    {
        foreach (Shape shape in shapes)
        {
            shape.Draw(e);
        }
    }
    public void SaveTo(string path)
    {
        StreamWriter writer;
        try
        {
            writer = File.CreateText(path);
        }
        catch
        {
            MessageBox.Show("Can't create file '{fileName}'!", "Error");
            return;
        }
        foreach (Shape s in shapes)
        {
            writer.WriteLine(s.Serialize());
        }
        writer.Close();
    }
    public void LoadFrom(string path)
    {
        StreamReader reader;
        try
        {
            reader = File.OpenText(path);
        }
        catch
        {
            MessageBox.Show($"Can't open file '{path}'!", "Error");
            return;
        }
        string s;

        Empty();
        while((s = reader.ReadLine()) != null)
        {
            Shape shape = Shape.Deserialize(s);
            if (shape != null)
            {
                shapes.Add(shape);
            }
        }
        reader.Close();
    }
}