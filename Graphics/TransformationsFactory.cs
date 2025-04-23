using System.Collections;
using Graphics.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Graphics;

public class TransformationsFactory : IEnumerable<ITransformation>
{
    public string ImageSrc { get; set; } = "assets/imgs/penguin.png";
    Image<Rgba32> image;
    private Rgba32[,] input;
    public TransformationsFactory()
    {
        ImageSrc  = "assets/imgs/penguin.png";
        image = ImageHelper.LoadPenguinImage();
        input = new Rgba32[image.Width, image.Height];
        ResetInput();
    }

    private List<ITransformation> Transformations { get; set; } 
        = new List<ITransformation>();

    public void AddTransformation(ITransformation transformation)
    {
        Transformations.Add(transformation);
    }
    
    public void Reset()
    {
        ImageSrc = "assets/imgs/penguin.png";
        Transformations.Clear();
    }

    public void DeleteTransformation(ITransformation t)
    {
        Transformations.Remove(t);
    }


    private void ResetInput()
    {
        input = new Rgba32[image.Width, image.Height];
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                input[x, y] = image[x, y];
            }
        }
    }

    public void ApplyAndSave()
    {
        Apply();
        SaveImage();
    }

    public void Apply()
    {
        ResetInput();
        foreach (var t in Transformations)
        {
            input = t.Apply(input);
        }
        
    }

    public void SaveImage()
    {
        using (Image<Rgba32> OutputImage = new Image<Rgba32>(image.Width, image.Height))
        {
            int width  = Math.Min(OutputImage.Width , input.GetLength(0));
            int height = Math.Min(OutputImage.Height, input.GetLength(1));
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    OutputImage[x, y] = input[x, y];
                }
            }

            ImageSrc = "/Generated/penguin.png";
            OutputImage.Save($"wwwroot/assets{ImageSrc}");
        }
    }
    public IEnumerator<ITransformation> GetEnumerator()
    {
        return Transformations.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}