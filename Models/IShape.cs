namespace Graphics.Models;

public interface IShape
{
    public IEnumerable<PointInfo> GetIndexes();
    public string? ImgSrc { get; set; }
    public AlgorithmType Algorithm { get; set; }
}