using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Cloud.FCM
{
    public class NotificationService : INotificationService
    {
        private IConfiguration Configuration { get; set; }

        public NotificationService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task PushNotification(string token, string title, string body, string imageUrl)
        {
            var app = InitializeFirebaseApp();
            var messaging = FirebaseMessaging.GetMessaging(app);

            await messaging.SendAsync(new Message
            {
                Token = token,
                Notification = new Notification
                {
                    Title = title,
                    Body = body,
                    ImageUrl = imageUrl

                }
            });
        }

        private FirebaseApp InitializeFirebaseApp()
        {
            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("feedopt-firebase-adminsdk-592d1-8df2e87481.json")
                .CreateScoped(Configuration["FcmApi"])
            });

            return app;
        }

    }
}
