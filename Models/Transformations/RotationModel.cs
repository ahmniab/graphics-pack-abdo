using System.ComponentModel.DataAnnotations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Models.Transformations;

public class RotationModel : Transformation
{
    [Required(ErrorMessage = "Please provide an angle in degrees.")]
    public double AngleDeg { get; set; }
    public string Name { get; set; } = "Rotation";

    public Rgba32[,] ApplyTransformation(Rgba32[,] input)
    {
        int width = input.GetLength(0);
        int height = input.GetLength(1);

        double radians = -AngleDeg * Math.PI / 180;
        double cos = Math.Cos(radians);
        double sin = Math.Sin(radians);

        // Calculate new dimensions for the rotated image
        int newWidth = (int)Math.Ceiling(Math.Abs(width * cos) + Math.Abs(height * sin));
        int newHeight = (int)Math.Ceiling(Math.Abs(width * sin) + Math.Abs(height * cos));

        Rgba32[,] output = new Rgba32[newWidth, newHeight];

        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                // Transform coordinates relative to top-left corner
                int srcX = (int)Math.Round(x * cos + y * sin);
                int srcY = (int)Math.Round(-x * sin + y * cos);

                output[x, y] = (srcX >= 0 && srcX < width && srcY >= 0 && srcY < height)
                    ? input[srcX, srcY]
                    : Color.Transparent;
            }
        }

        return output;
    }
}