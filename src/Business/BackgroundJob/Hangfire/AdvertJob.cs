using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using DataAccess.Concretes;
using Entity.Concretes;

namespace Business.BackgroundJob.Hangfire
{
    internal class AdvertJob : IActiveJob
    {

        public void UpdateAdvertStatusJob(IAdvertService service,Advert advert)
        {
            global::Hangfire.BackgroundJob.Schedule(() => UpdateStatus(service,advert),DateTime.Now.AddDays(30));
        }

        public void UpdateStatus(IAdvertService service, Advert advert)
        {
            service.UpdateStatus(advert);
        }
    }
}
