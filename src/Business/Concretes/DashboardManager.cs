using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Core.Aspects.Autofac.Cache;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Concretes
{
    internal class DashboardManager : IDashboardService
    {
        private readonly IAdvertService _advertService;
        private readonly IUserService _userService;
        public DashboardManager(IAdvertService advertService, IUserService userService)
        {
            _advertService = advertService;
            _userService = userService;
        }

        [SecuredOperation($"{Role.User},{Role.Admin},{Role.SuperAdmin}")]
        [CacheAspect(15)]
        public IDataResult<DashboardDto> GetDashboard()
        {
            var advert = _advertService.GetAll();
            var activeAdverts = _advertService.GetAll(a => a.Status == Status.Active);
            var pendingAdverts = _advertService.GetAll(a => a.Status == Status.Pending);
            var users = _userService.GetAll();

            return new SuccessDataResult<DashboardDto>(new DashboardDto
            {
                AdvertQuantity = advert.Data.Count,
                ActiveAdvertQuantity = activeAdverts.Data.Count,
                PendingAdvertQuantity = pendingAdverts.Data.Count,
                UserQuantity = users.Data.Count
            });
        }
    }
}
