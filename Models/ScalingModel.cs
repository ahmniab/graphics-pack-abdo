using System.ComponentModel.DataAnnotations;
using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Models;

public class ScalingModel : ITransformation
{
    public string Name { get; set; } = "Scale";
    [Required(ErrorMessage = "Please provide a value for the scale X.")]
    [Range(0.1, double.MaxValue, ErrorMessage = "Value must be at least 0.1 to prevent division by zero.")]

    public double ScaleX { get; set; }
    [Required(ErrorMessage = "Please provide a value for the scale Y.")]
    [Range(0.1, double.MaxValue, ErrorMessage = "Value must be at least 0.1 to prevent division by zero.")]
    public double ScaleY { get; set; }
    public Rgba32[,] Apply(Rgba32[,] input)
    {
        int originalWidth = input.GetLength(0);
        int originalHeight = input.GetLength(1);

        int newWidth = (int)(originalWidth * ScaleX);
        int newHeight = (int)(originalHeight * ScaleY);

        Rgba32[,] output = new Rgba32[newWidth, newHeight];

        for (int x = 0; x < newWidth; x++)
        {
            for (int y = 0; y < newHeight; y++)
            {
                double originalX = x / ScaleX;
                double originalY = y / ScaleY;

                int sourceX = (int)Math.Clamp(originalX, 0, originalWidth - 1);
                int sourceY = (int)Math.Clamp(originalY, 0, originalHeight - 1);

                output[x, y] = input[sourceX, sourceY];
            }
        }

        return output;
    }
}