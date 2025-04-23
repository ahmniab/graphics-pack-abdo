using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Models;

public interface ITransformation
{
    public string Name { get; set; }
    public Rgba32 [,] Apply(Rgba32 [,] input);
}