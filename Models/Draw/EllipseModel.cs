namespace Graphics.Models.Draw;

public class EllipseModel : Shape
{
    public IEnumerable<Point> GetAllPoints()
    { 
        return midptellipse();
    }

    public string? ImgSrc { get; set; }
    public double x { get; set; }
    public double y { get; set; }
    public double Rx { get; set; }
    public double Ry { get; set; }
    
    IEnumerable<EllipsePoint> midptellipse()
    {
     
        double dx, dy, d1, d2, x, y, oldp;
        x = 1;
        y = Ry;
     
        // Initial decision parameter of region 1
        d1 = (Ry * Ry) - (Rx * Rx * Ry) +
            (0.25f * Rx * Rx);
        dx = 2 * Ry * Ry * x;
        dy = 2 * Rx * Rx * y;
        oldp = d1;

        yield return new EllipsePoint {x = 0 , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePoint {x = 0 , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePoint {x = 0 , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePoint {x = 0 , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        
        yield return new EllipsePoint {x = ( x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePoint {x = ( -x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePoint {x = ( x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        yield return new EllipsePoint {x = ( -x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};

        // For region 1
        while (dx < dy)
        {                 
            if (oldp < 0)
            {
                d1 = d1 + dx + (Ry * Ry);
            }
            else
            {
                d1 = d1 + (dx - dy + (Ry * Ry));
            }
            oldp = d1;
            if (d1 >= 0)
            {
                y--;
                dy = 2 * Rx * Rx * y;
            }
            x++;
            dx = 2 * Ry * Ry * x;
            // Checking and updating value of
            // decision parameter based on algorithm
            // points based on 4-way symmetry
            yield return new EllipsePoint {x = ( x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
            yield return new EllipsePoint {x = (-x ) , y = ( y ) , PK = d1 , dx = dx , dy = dy};
            yield return new EllipsePoint {x = ( x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};
            yield return new EllipsePoint {x = (-x ) , y = (-y ) , PK = d1 , dx = dx , dy = dy};
        }
    
        // Decision parameter of region 2
        d2 = (Ry * Ry) * (((x + 0.5f) * (x + 0.5f)))
            + ((Rx * Rx) * ((y - 1) * (y - 1)))
            - (Rx * Rx * Ry * Ry);
        oldp = d2;
        if (d2 < 0){
            x++;
        }
        y--;
        dx = 2 * Ry * Ry * x;
        dy = 2 * Rx * Rx * y;
        // Plotting points of region 2
        while (y >= 0)
        {
     
            // points based on 4-way symmetry
            yield return new EllipsePoint {x = ( x )  , y = ( y ) , PK = d2 , dx = dx , dy = dy};
            yield return new EllipsePoint {x = (-x )  , y = ( y ) , PK = d2 , dx = dx , dy = dy};
            yield return new EllipsePoint {x = ( x )  , y = (-y ) , PK = d2 , dx = dx , dy = dy};
            yield return new EllipsePoint {x = (-x )  , y = (-y ) , PK = d2 , dx = dx , dy = dy};

            // Checking and updating parameter
            // value based on algorithm
            if (oldp > 0)
            {
                d2 = d2 + (Rx * Rx) - dy;
            }
            else
            {
                d2 = d2 + dx - dy + (Rx * Rx);
            }
            oldp = d2;
            if (d2 < 0){
                x++;
                dx = 2 * Ry * Ry * x;
            }
            y--;
            dy = 2 * Rx * Rx * y;
        }
    }
}