namespace FoodHub.Blazor.Client.Services
{
    public enum ToastLevel { 
        
        Info,
        Success,
        Warning,
        Error
    }
    public class ToastMessage {

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public ToastLevel Level { get; set; } = ToastLevel.Error;
    }
    public class ToastService
    {
        public event Action<ToastMessage>? OnShow;

        public void ShowError(string message, string title = "系统提示") 
        {
            ShowToast(message,title,ToastLevel.Error);
        }

        public void ShowWarning(string message, string title = "警告")
        {
            ShowToast(message, title, ToastLevel.Warning);
        }

        public void ShowSuccess(string message, string title = "成功")
        {
            ShowToast(message, title, ToastLevel.Success);
        }

        private void ShowToast(string message, string title,ToastLevel level)
        {
            OnShow?.Invoke(new ToastMessage
            {
                Title = title,

                Message = message,

                Level = level
            });
        }
    }
}
