
namespace Graphics.Models.Draw;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class ImageBuilder
{
    public Image<Rgba32> ImageMatrex { get; } = new Image<Rgba32>(500, 500);
    private Rgba32 BgColor = new Rgba32(0, 0, 0, 0  );
    private Rgba32 FgColor = new Rgba32(0, 0, 0, 255);
    public Shape Shape;
    
    public string Save(string filename)
    {
        ImageMatrex.Save($"wwwroot/assets/Generated/{filename}.png");
        return $"/Generated/{filename}.png";
    } 
    
    public ImageBuilder SetShape(Shape shape)
    {
        Shape = shape;
        return this;
    }

    private void FillBg()
    {
        for (int x = 0; x < ImageMatrex.Width; x++)
        {
            for (int y = 0; y < ImageMatrex.Height; y++)
            {
                ImageMatrex[x, y] = BgColor;
            }
        }
    }
    
    private void FillShape()
    {
        foreach (var p in Shape.GetAllPoints())
        {
            try
            {
                ImageMatrex[p.GetXOnImageMatrex(), p.GetYOnImageMatrex()] = FgColor;
            }
            catch (OutOfImageBoundException) { }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
    
    public ImageBuilder Builed()
    {
        FillBg();
        FillShape();
        return this;
    }


}