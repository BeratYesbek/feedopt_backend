using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Cloud.FCM
{
    public interface INotificationService
    {
        Task PushNotification(string token, string title, string body, string imageUrl);
    }
}
