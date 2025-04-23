namespace Graphics.Models;

public class ShapeList : IShapeList
{ 
    public IEnumerable<IShape> Shapes { get; set; }

    public ShapeList()
    {
        this.Shapes = new List<IShape>();
    }
    public void AddShape(IShape shape)
    {
        (this.Shapes as List<IShape>).Add(shape);
    }

    public void RemoveShape(int Hash)
    {
        (this.Shapes as List<IShape>).RemoveAll(
            s => s.GetHashCode() == Hash
            );
    }

    public void Clear()
    {
        (this.Shapes as List<IShape>).Clear();
    }
}