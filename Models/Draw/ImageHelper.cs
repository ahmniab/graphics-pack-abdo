using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Graphics.Models.Draw;

public static class ImageHelper
{
    private static int ImageId = 0;
    

    public static int GetXOnImageMatrex(this Point point)
    {
        int FinalX = (int)Math.Round(point.x);
        FinalX += 250;
        if (FinalX >= 500 || FinalX < 0)
            throw new OutOfImageBoundException();
        return FinalX;
    }
    public static int GetYOnImageMatrex(this Point point)
    {
        int FinalY = (int)Math.Round(point.y);
        FinalY = 250 - FinalY;
        if (FinalY >= 500 || FinalY < 0)
            throw new OutOfImageBoundException();
        return FinalY;
    }
    public static Image<Rgba32> LoadTImage()
    {
        return Image.Load<Rgba32>("wwwroot/assets/imgs/TImage.png");
    }
        
}
