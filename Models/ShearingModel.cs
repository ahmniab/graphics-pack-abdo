using System.ComponentModel.DataAnnotations;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Graphics.Models;

public class ShearingModel : ITransformation
{
    public string Name { get; set; } = "Shearing";
    
    [Required(ErrorMessage = "Please provide a value for horizontal shear.")]
    [Range(-10.0, 10.0, ErrorMessage = "Shear value must be between -10 and 10.")]
    public double ShearX { get; set; } = 0.0;
    
    [Required(ErrorMessage = "Please provide a value for vertical shear.")]
    [Range(-10.0, 10.0, ErrorMessage = "Shear value must be between -10 and 10.")]
    public double ShearY { get; set; } = 0.0;

    public Rgba32[,] Apply(Rgba32[,] input)
    {
        int width = input.GetLength(0);
        int height = input.GetLength(1);
        
        // Calculate output dimensions to contain the sheared image
        int newWidth = (int)(width + Math.Abs(height * ShearX));
        int newHeight = (int)(height + Math.Abs(width * ShearY));
        
        Rgba32[,] output = new Rgba32[newWidth, newHeight];
        
        // Calculate center offset for proper positioning
        double centerX = width / 2.0;
        double centerY = height / 2.0;
        double newCenterX = newWidth / 2.0;
        double newCenterY = newHeight / 2.0;

        for (int x = 0; x < newWidth; x++)
        {
            for (int y = 0; y < newHeight; y++)
            {
                // Transform coordinates with inverse shear
                double origX = (x - newCenterX) - ShearX * (y - newCenterY) + centerX;
                double origY = (y - newCenterY) - ShearY * (x - newCenterX) + centerY;
                
                int sourceX = (int)Math.Clamp(origX, 0, width - 1);
                int sourceY = (int)Math.Clamp(origY, 0, height - 1);
                
                if (sourceX >= 0 && sourceX < width && sourceY >= 0 && sourceY < height)
                {
                    output[x, y] = input[sourceX, sourceY];
                }
                else
                {
                    output[x, y] = new Rgba32(0, 0, 0, 0); // Transparent for out-of-bounds
                }
            }
        }

        return output;
    }
}