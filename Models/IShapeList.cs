namespace Graphics.Models;

public interface IShapeList
{
    public IEnumerable<IShape> Shapes { get; set; }
    public void AddShape(IShape shape);
    public void RemoveShape(int Hash);
    public void Clear();
    
}