using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    /// <summary>
    /// Disabled Service
    /// </summary>
    public interface ISmsService
    {
        Task SendSms(string title,string phoneNumber, string text);
    }
}
