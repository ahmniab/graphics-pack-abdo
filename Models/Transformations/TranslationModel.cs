using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Models.Transformations;

public class TranslationModel : Transformation
{
    public string Name { get; set; } = "Translation";
    public int delta_x;
    public int delta_y;
    
    public Rgba32[,] ApplyTransformation(Rgba32[,] input)
    {
        int newWidth = input.GetLength(0) + Math.Abs(delta_x);
        int newHeight = input.GetLength(1) + Math.Abs(delta_y);
        Rgba32[,] output = new Rgba32[newWidth, newHeight];

        for (int x = 0; x < input.GetLength(0); x++)
        {
            for (int y = 0; y < input.GetLength(1); y++)
            {
                int newX = x + delta_x;
                int newY = y + delta_y;

                if (newX >= 0 && newX < newWidth && newY >= 0 && newY < newHeight)
                {
                    output[newX, newY] = input[x, y];
                }
            }
        }

        return output;
    }

}