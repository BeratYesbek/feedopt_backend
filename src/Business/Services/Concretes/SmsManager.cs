﻿using Business.Services.Abstracts;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Business.Services.Concretes
{
    public class SmsManager : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public SmsManager(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task SendSms(string title, string phoneNumber, string text)
        {
           var result = await _httpClient.PostAsync(_configuration["SmsApiURL"], new StringContent(JsonConvert.SerializeObject(new
            {

            }), Encoding.UTF8, "application/json"));
        }
    }
}
