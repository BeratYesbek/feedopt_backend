using System.Threading.Tasks;

namespace Core.Utilities.Cloud.FCM
{
    public interface INotificationService
    {
        Task PushNotification(string token, string title, string body, string imageUrl);
    }
}
