namespace Graphics.Models.Draw;

public class CircleModel : Shape
{
    public IEnumerable<Point> GetAllPoints()
    {
        return CircleBres();
    }

    public string? ImgSrc { get; set; }
    
    public int X { get; set; }
    public int Y { get; set; }
    public int Radius { get; set; }

    private IEnumerable<BresPoint> CircleBres()
    {

            int x = 1, y = Radius;
            int Pk = 1 - Radius;
            int OldP = Pk;
            yield return new BresPoint{x = X-0, y = Y+y, Pk = OldP};
            yield return new BresPoint{x = X+0, y = Y+y, Pk = OldP};
            yield return new BresPoint{x = X+0, y = Y-y, Pk = OldP};
            yield return new BresPoint{x = X-0, y = Y-y, Pk = OldP};
            yield return new BresPoint{x = X+y, y = Y+x, Pk = OldP};
            yield return new BresPoint{x = X-y, y = Y+x, Pk = OldP};
            yield return new BresPoint{x = X+y, y = Y-x, Pk = OldP};
            yield return new BresPoint{x = X-y, y = Y-x, Pk = OldP};
            
            
            while (y >= x){

                // Draw the circle using the new coordinates
                yield return new BresPoint{x = X+x, y = Y+y, Pk = OldP};
                yield return new BresPoint{x = X-x, y = Y+y, Pk = OldP};
                yield return new BresPoint{x = X+x, y = Y-y, Pk = OldP};
                yield return new BresPoint{x = X-x, y = Y-y, Pk = OldP};
                yield return new BresPoint{x = X+y, y = Y+x, Pk = OldP};
                yield return new BresPoint{x = X-y, y = Y+x, Pk = OldP};
                yield return new BresPoint{x = X+y, y = Y-x, Pk = OldP};
                yield return new BresPoint{x = X-y, y = Y-x, Pk = OldP};

                if (OldP > 0) {
                    Pk = Pk + (2 * x) + 1 - (2 * y);
                }
                else
                    Pk = Pk + 2 * x + 1;

                if (Pk > 0){
                    y--;
                }
                x++;
                OldP = Pk;
            }
    }
    
    
}