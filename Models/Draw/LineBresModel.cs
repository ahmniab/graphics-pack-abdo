using System.Runtime.CompilerServices;

namespace Graphics.Models.Draw;

public class LineBresModel : Shape
{
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }

    public string? ImgSrc { get; set; }

    public IEnumerable<Point> GetAllPoints()
    {
        return Bresenham();
    }

    private int OctantNumber = -1; 
    private int IncX = 1; 
    private int IncY = 1; 
    private void SetOctantNumber()
    {
        double slope = (Y2 - Y1) / (double)(X2 - X1);
        if (0 < slope && slope <= 1) // Octant 1 or 5
        {
            if (X1 < X2)
                OctantNumber = 1;
            else
                OctantNumber = 5;
            
        }else if (slope > 1)        // Octant 2 or 6
        {
            if (Y1 < Y2)
                OctantNumber = 2;
            else
                OctantNumber = 6;
            
        }else if (slope < -1)       // Octant 3 or 7
        {
            if (Y1 < Y2)
                OctantNumber = 3;
            else
                OctantNumber = 7;
            
        }else                       // Octant 4 or 8
        {
            if (X1 < X2)
                OctantNumber = 8;
            else
                OctantNumber = 4;
        }
        
    }

    private void SwapXY()
    {
        (X1, Y1) = (Y1, X1);
        (X2, Y2)     = (Y2, X2);
    }
    private void MakeLineInOctantOne()
    {
        SetOctantNumber();
        switch (OctantNumber)
        {
            case 2:
                SwapXY();
                break;
            case 3:
                SwapXY();
                IncY = -1;
                break;
            case 4:
                IncX = -1;
                break;
            case 5:
                IncX = -1;
                IncY = -1;
                break;
            case 6:
                SwapXY();
                IncX = -1;
                IncY = -1;
                break;
            case 7:
                SwapXY();
                IncX = -1;
                break;
            case 8:
                IncY = -1;
                break;
        }
    }

    private void SwapBackIfShouldTo(BresPoint p)
    {
        if (OctantNumber == 2 || 
            OctantNumber == 3 || 
            OctantNumber == 6 || 
            OctantNumber == 7)
        {
            (p.x, p.y) = (p.y, p.x);
        }
    }
    private void ResetXY()
    {
        if (OctantNumber == 2 || 
            OctantNumber == 3 || 
            OctantNumber == 6 || 
            OctantNumber == 7)
        {
            SwapXY();
        }
    }
    private bool IsLineEnd(int x) 
        => Math.Abs(x - X2) == 1 || x - X2 == 0;
        
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    private IEnumerable<BresPoint> Bresenham()
    {
        MakeLineInOctantOne();
        BresPoint point = new();
        int dx = Math.Abs(X2 - X1), dy = Math.Abs(Y2 - Y1);
        int x, y, p = 2 * dy - dx;
        int twoDy = 2 * dy, twoDyMinusDx = 2 * (dy - dx);
        
        x = X1;
        y = Y1;        

        while (!IsLineEnd(x))
        {
            point.Pk = p;
            x += IncX;
            if (p < 0)
                p += twoDy;
            else
            {
                y += IncY;
                p += twoDyMinusDx;
            }

            point.x = x;
            point.y = y;
            SwapBackIfShouldTo(point);
            yield return point;
            point = new();
        }
        ResetXY();
    }

    
}