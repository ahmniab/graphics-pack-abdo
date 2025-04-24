using System.Runtime.CompilerServices;

namespace Graphics.Models.Draw;

public class LineDDAModel : Shape
{
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }

    public string? ImgSrc { get; set; }

    public IEnumerable<Point> GetAllPoints()
    {
        return DDA();
    }
    
    private IEnumerable<Point> DDA()
    {
        int dx = X2 - X1, dy = Y2 - Y1, steps, k;
        double xIncrement, yIncrement, x = X1, y = Y1;

        if (Math.Abs(dx) > Math.Abs(dy))
            steps = Math.Abs(dx);
        else
            steps = Math.Abs(dy);

        xIncrement = (double)dx / steps;
        yIncrement = (double)dy / steps;

        for (k = 0; k < steps; k++)
        {
            x += xIncrement;
            y += yIncrement;
            yield return new Point
            {
                x = x,
                y = y,
            };
        }
    }
}