using Core.DataAccess;
using Core.Entity.Concretes;
using DataAccess.Abstracts;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfLogDal : EfEntityRepositoryBase<Log, AppDbContext>, ILogDal
    {
        public List<LogReadDto> GetLogDetails(Expression<Func<Log, bool>> filter = null)
        {
            using (var context = new AppDbContext())
            {
                var result = from log in context.logs
                             join user in context.Users on int.Parse(log.userid) equals user.Id
                             select new LogReadDto
                             {
                                 Id = log.Id,
                                 message = log.message,
                                 level = log.level,
                                 logger = log.logger,
                                 claims = log.claims,
                                 machinename = log.machinename,
                                 email = log.email,
                                 userid = log.userid,
                                 methodname = log.methodname,
                                 fullname = log.fullname,
                                 User = user,
                                 logdetail = log.logdetail,
                                 logparameters = log.logparameters,
                                 stacktrace = log.stacktrace,
                             };
                return result.ToList();
            }
        }
    }
}
