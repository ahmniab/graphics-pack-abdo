using System.Runtime.CompilerServices;

namespace Graphics.Models;

public class LineModel : IShape
{
    public int XStart { get; set; }
    public int YStart { get; set; }
    public int XEnd { get; set; }
    public int YEnd { get; set; }

    public string name { get; set; } = "Line";
    public string? color { get; set; }

    public string? ImgSrc { get; set; }
    public AlgorithmType Algorithm { get; set; }

    public IEnumerable<PointInfo> GetIndexes()
    {
        switch (Algorithm)
        {
            case AlgorithmType.DDALine:
                return DDA();
            case AlgorithmType.BresenhamLine:
                return Bresenham();
            default:
                throw new NonValidAlgorithmException();
        }
    }

    private int OctantNumber = -1; 
    private int IncX = 1; 
    private int IncY = 1; 
    private void SetOctantNumber()
    {
        double slope = (YEnd - YStart) / (double)(XEnd - XStart);
        if (0 < slope && slope < 1) // Octant 1 or 5
        {
            if (XStart < XEnd)
                OctantNumber = 1;
            else
                OctantNumber = 5;
            
        }else if (slope > 1)        // Octant 2 or 6
        {
            if (YStart < YEnd)
                OctantNumber = 2;
            else
                OctantNumber = 6;
            
        }else if (slope < -1)       // Octant 3 or 7
        {
            if (YStart < YEnd)
                OctantNumber = 3;
            else
                OctantNumber = 7;
            
        }else                       // Octant 4 or 8
        {
            if (XStart < XEnd)
                OctantNumber = 8;
            else
                OctantNumber = 4;
        }
        
    }

    private void SwapXY()
    {
        (XStart, YStart) = (YStart, XStart);
        (XEnd, YEnd)     = (YEnd, XEnd);
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

    private void SwapBackIfShouldTo(BresPointInfo p)
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
        => Math.Abs(x - XEnd) == 1 || x - XEnd == 0;
        
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    private IEnumerable<BresPointInfo> Bresenham()
    {
        MakeLineInOctantOne();
        BresPointInfo PointInfo = new();
        int dx = Math.Abs(XEnd - XStart), dy = Math.Abs(YEnd - YStart);
        int x, y, p = 2 * dy - dx;
        int twoDy = 2 * dy, twoDyMinusDx = 2 * (dy - dx);
        
        x = XStart;
        y = YStart;        

        while (!IsLineEnd(x))
        {
            PointInfo.Pk = p;
            x += IncX;
            if (p < 0)
                p += twoDy;
            else
            {
                y += IncY;
                p += twoDyMinusDx;
            }

            PointInfo.x = x;
            PointInfo.y = y;
            SwapBackIfShouldTo(PointInfo);
            yield return PointInfo;
            PointInfo = new();
        }
        ResetXY();
    }

    private IEnumerable<PointInfo> DDA()
    {
        int dx = XEnd - XStart, dy = YEnd - YStart, steps, k;
        double xIncrement, yIncrement, x = XStart, y = YStart;

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
            yield return new PointInfo
            {
                x = x,
                y = y,
            };
        }
    }
}