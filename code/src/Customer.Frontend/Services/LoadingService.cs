namespace IApply.Frontend.Services;

public class LoadingService
{
    public event Action? OnChange;
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        private set
        {
            _isLoading = value;
            NotifyStateChanged();
        }
    }

    public void Show()
    {
        IsLoading = true;
    }

    public void Hide()
    {
        IsLoading = false;
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
