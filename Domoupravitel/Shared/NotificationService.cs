namespace Domoupravitel.Shared
{
    public class NotificationService
    {
        public event Action<string, NotificationType>? OnShow;
        public Guid InstanceId { get; } = Guid.NewGuid();

        public void ShowSuccess(string message) => OnShow?.Invoke(message, NotificationType.Success);
        public void ShowInfo(string message) => OnShow?.Invoke(message, NotificationType.Info);
        public void ShowWarning(string message) => OnShow?.Invoke(message, NotificationType.Warning);
        public void ShowError(string message) => OnShow?.Invoke(message, NotificationType.Error);
    }
}
