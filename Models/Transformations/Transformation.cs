using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Models.Transformations;

public interface Transformation
{
    public string Name { get; set; }
    public Rgba32 [,] ApplyTransformation(Rgba32 [,] input);
}