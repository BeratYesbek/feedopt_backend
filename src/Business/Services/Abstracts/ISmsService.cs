using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ISmsService
    {
        Task SendSms(string title,string phoneNumber, string text);
    }
}
