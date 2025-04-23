namespace Graphics.Models;

public class EllipseModel : IShape
{
    public IEnumerable<PointInfo> GetIndexes()
    {
        if (Algorithm == AlgorithmType.MidPointEllipse)
        {
            return midptellipse();
        }
        else
            throw new NonValidAlgorithmException();
    }

    public string name { get; set; } = "Ellipse";
    public string? color { get; set; }
    public string? ImgSrc { get; set; }
    public AlgorithmType Algorithm { get; set; } = AlgorithmType.MidPointEllipse;
    public double X { get; set; }
    public double Y { get; set; }
    public double RX { get; set; }
    public double RY { get; set; }
    
    IEnumerable<EllipsePointInfo> midptellipse()
    {
     
        double dx, dy, d1, d2, x, y, oldp;
        x = 1;
        y = RY;
     
        // Initial decision parameter of region 1
        d1 = (RY * RY) - (RX * RX * RY) +
            (0.25f * RX * RX);
        dx = 2 * RY * RY * x;
        dy = 2 * RX * RX * y;
        oldp = d1;

        yield return new EllipsePointInfo {x = 0 , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePointInfo {x = 0 , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePointInfo {x = 0 , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePointInfo {x = 0 , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        
        yield return new EllipsePointInfo {x = ( x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePointInfo {x = ( -x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePointInfo {x = ( x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePointInfo {x = ( -x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};

        // For region 1
        while (dx < dy)
        {                 
            if (oldp < 0)
            {
                d1 = d1 + dx + (RY * RY);
            }
            else
            {
                d1 = d1 + (dx - dy + (RY * RY));
            }
            oldp = d1;
            if (d1 >= 0)
            {
                y--;
                dy = 2 * RX * RX * y;
            }
            x++;
            dx = 2 * RY * RY * x;
            // Checking and updating value of
            // decision parameter based on algorithm
            // points based on 4-way symmetry
            yield return new EllipsePointInfo {x = ( x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
            yield return new EllipsePointInfo {x = (-x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
            yield return new EllipsePointInfo {x = ( x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};
            yield return new EllipsePointInfo {x = (-x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        }
    
        // Decision parameter of region 2
        d2 = (RY * RY) * (((x + 0.5f) * (x + 0.5f)))
            + ((RX * RX) * ((y - 1) * (y - 1)))
            - (RX * RX * RY * RY);
        oldp = d2;
        if (d2 < 0){
            x++;
        }
        y--;
        dx = 2 * RY * RY * x;
        dy = 2 * RX * RX * y;
        // Plotting points of region 2
        while (y >= 0)
        {
     
            // points based on 4-way symmetry
            yield return new EllipsePointInfo {x = ( x )  , y = ( y ) , PK = d2 , dx = dx , dy = dy};
            yield return new EllipsePointInfo {x = (-x )  , y = ( y ) , PK = d2 , dx = dx , dy = dy};
            yield return new EllipsePointInfo {x = ( x )  , y = (-y ) , PK = d2 , dx = dx , dy = dy};
            yield return new EllipsePointInfo {x = (-x )  , y = (-y ) , PK = d2 , dx = dx , dy = dy};

            // Checking and updating parameter
            // value based on algorithm
            if (oldp > 0)
            {
                d2 = d2 + (RX * RX) - dy;
            }
            else
            {
                d2 = d2 + dx - dy + (RX * RX);
            }
            oldp = d2;
            if (d2 < 0){
                x++;
                dx = 2 * RY * RY * x;
            }
            y--;
            dy = 2 * RX * RX * y;
        }
    }
}