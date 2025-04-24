namespace Graphics.Models.Transformations;

public class TransformationsState
{
    public event Action OnChange;

    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}