namespace Graphics.Models;

public class CircleModel : IShape
{
    public IEnumerable<PointInfo> GetIndexes()
    {
        if (Algorithm == AlgorithmType.BresenhamCircle)
        {
            return CircleBres();
        }
        else
            throw new NonValidAlgorithmException();
    }

    public string? name { get; set; }
    public string? color { get; set; }
    public string? ImgSrc { get; set; }
    public AlgorithmType Algorithm { get; set; } = AlgorithmType.BresenhamCircle;
    
    public int X { get; set; }
    public int Y { get; set; }
    public int Radius { get; set; }

    private IEnumerable<BresPointInfo> CircleBres()
    {

            int x = 1, y = Radius;
            int Pk = 1 - Radius;
            int OldP = Pk;
            yield return new BresPointInfo{x = X-0, y = Y+y, Pk = OldP};
            yield return new BresPointInfo{x = X+0, y = Y+y, Pk = OldP};
            yield return new BresPointInfo{x = X+0, y = Y-y, Pk = OldP};
            yield return new BresPointInfo{x = X-0, y = Y-y, Pk = OldP};
            yield return new BresPointInfo{x = X+y, y = Y+x, Pk = OldP};
            yield return new BresPointInfo{x = X-y, y = Y+x, Pk = OldP};
            yield return new BresPointInfo{x = X+y, y = Y-x, Pk = OldP};
            yield return new BresPointInfo{x = X-y, y = Y-x, Pk = OldP};
            
            
            while (y >= x){

                // Draw the circle using the new coordinates
                yield return new BresPointInfo{x = X+x, y = Y+y, Pk = OldP};
                yield return new BresPointInfo{x = X-x, y = Y+y, Pk = OldP};
                yield return new BresPointInfo{x = X+x, y = Y-y, Pk = OldP};
                yield return new BresPointInfo{x = X-x, y = Y-y, Pk = OldP};
                yield return new BresPointInfo{x = X+y, y = Y+x, Pk = OldP};
                yield return new BresPointInfo{x = X-y, y = Y+x, Pk = OldP};
                yield return new BresPointInfo{x = X+y, y = Y-x, Pk = OldP};
                yield return new BresPointInfo{x = X-y, y = Y-x, Pk = OldP};

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