namespace Graphics.Models.Draw;

public interface Shape
{
    public IEnumerable<Point> GetAllPoints();
    public string? ImgSrc { get; set; }
}