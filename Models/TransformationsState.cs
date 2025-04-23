namespace Graphics.Models;

public class TransformationsState
{
    public event Action OnChange;

    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}