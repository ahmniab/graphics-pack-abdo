using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Models;

public class ReflectionModel : ITransformation
{
    public string Name { get; set; } = "Reflection";
    public bool HorizontalReflection = false;
    public bool VerticalReflection   = false;  
    
    private static Rgba32[,] ReflectHorizontally(Rgba32[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);
    
        Rgba32[,] reflected = new Rgba32[rows, cols];
    
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                reflected[i, j] = input[i, cols - 1 - j];
            }
        }
    
        return reflected;
    }
    
    public static Rgba32[,] ReflectVertically(Rgba32[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);
    
        Rgba32[,] reflected = new Rgba32[rows, cols];
    
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                reflected[i, j] = input[rows - 1 - i, j];
            }
        }
    
        return reflected;
    }
    public Rgba32[,] Apply(Rgba32[,] input)
    {
        if (HorizontalReflection)
            input = ReflectHorizontally(input);
        if (VerticalReflection)
            input = ReflectVertically(input);
        return input;
    }
}

