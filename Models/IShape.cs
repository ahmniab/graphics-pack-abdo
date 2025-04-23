namespace Graphics.Models;

public interface IShape
{
    public IEnumerable<PointInfo> GetIndexes();
    public string name { get; set; } 
    public string color { get; set; }
    public string? ImgSrc { get; set; }
    public AlgorithmType Algorithm { get; set; }
}