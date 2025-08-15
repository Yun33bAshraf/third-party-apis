namespace IApply.Frontend.Services
{
    public class AlertService
    {
        public event Func<string, string, Task> OnShow;

        public void Show(string message, string alertClass = "alert-success bg-success")
        {
            OnShow?.Invoke(message, alertClass);
        }

        public void ShowError(string message, string alertClass = "alert-danger bg-danger")
        {
            OnShow?.Invoke(message, alertClass);
        }
    }
}
