using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.BusinessRules;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Result.Abstracts.IResult;
using Entity.Dtos.Filter;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using Business.Filters;
using Business.Messages.MethodMessages;
using Business.Services.Abstracts;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concretes
{

    /// <summary>
    /// This class is working with dependency injection to manage our Advert using business logic
    /// Moreover, this class includes aspect oriented programming design, Therefore Each of the methods is going to process something before the runtime or after the runtime
    /// </summary>
    public class AdvertManager : AdvertFilter, IAdvertService
    {
        private readonly IAdvertDal _advertDal;

        private readonly IAdvertImageService _imageService;

        private readonly ILocationService _locationService;

        private readonly ITelegramService _telegramService;

        private readonly IUserService _userService;

        public AdvertManager(IAdvertDal advertDal, IAdvertImageService imageService, ILocationService locationService,
            ITelegramService telegramService, IUserService userService)
        {
            _imageService = imageService;
            _advertDal = advertDal;
            _locationService = locationService;
            _telegramService = telegramService;
            _userService = userService;

        }


        /// <summary>
        /// This method run to add new Advert to the database. During the process it will add location and images.
        /// It is going to work with O(n)
        /// </summary>
        /// <param name="advert">advert</param>
        /// <param name="advertImage">advertImage</param>
        /// <param name="files">files</param>
        /// <param name="location">location</param>
        /// <returns>It will return data result that includes added advert</returns>
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AdvertValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail", Priority = 5)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById", Priority = 6)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter", Priority = 7)]
        [CacheRemoveAspect("IAdvertService.GetAll", Priority = 8)]
        public async Task<IDataResult<Advert>> Add(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location)
        {
            var ruleResult = Core.Utilities.Business.BusinessRules.Run(
                AdvertBusinessRules.EmailConfirmedForCreateAdvert(),
                AdvertBusinessRules.CheckFilesSize(files),
                AdvertBusinessRules.CheckDescriptionIllegalKeyword(advert.Description));

            advert.UserId = CurrentUser.User.Id;

            if (!ruleResult.Success)
                return new ErrorDataResult<Advert>(null, ruleResult.Message);

            var locationResult = _locationService.Add(location);

            if (locationResult is null)
                return new SuccessDataResult<Advert>(null, AdvertMessages.AdvertAdd);

            advert.LocationId = locationResult.Data.Id;
            var result = _advertDal.Add(advert);
            if (result is not null)
            {
                foreach (var file in files)
                {
                    var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                    var fileResult = await fileHelper.UploadAsync(file);
                    if (fileResult.Success)
                    {
                        var resultImage = _imageService.Add(new AdvertImage
                        {
                            ImagePath = fileResult.Message.Split("&&")[0],
                            PublicId = fileResult.Message.Split("&&")[1],
                            AdvertId = result.Id
                        });
                        if (!resultImage.Success)
                        {
                            return new ErrorDataResult<Advert>(null, AdvertMessages.AdvertAdvertFailed);
                        }
                    }
                }

                //Job.Create<AdvertJob>().UpdateAdvertStatusJob(this, result);
                // telegram service is going to send a message our telegram channel
                await _telegramService.SendNewPostAsync(advert, CurrentUser.User);

                return new SuccessDataResult<Advert>(result, AdvertMessages.AdvertAdd);
            }

            return new ErrorDataResult<Advert>(null, AdvertMessages.AdvertAdvertFailed);
        }


        /// <summary>
        /// This method run to update status so It will be delete in the system.
        /// This method is going to work O(3)
        /// </summary>
        /// <param name="advert"></param>
        /// <returns>It will return result that includes message</returns>
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail", Priority = 4)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById", Priority = 5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter", Priority = 6)]
        [CacheRemoveAspect("IAdvertService.GetAll", Priority = 7)]
        public IResult Delete(Advert advert)
        {
            advert.IsDeleted = true;
            _advertDal.Update(advert);
            return new SuccessResult(AdvertMessages.AdvertDelete);
        }

        /// <summary>
        ///  This method to run get Advert by ID, It is going to work with O(4) without Linq expression
        ///  This method should use if you don't need to detail of advert because of more efficient and faster
        /// </summary>
        /// <param name="id">advertId</param>
        /// <returns>It will return data result that includes an advert</returns>
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        public IDataResult<Advert> Get(int id)
        {
            var data = _advertDal.Get(a => a.Id == id);
            return new SuccessDataResult<Advert>(data);

        }

        /// <summary>
        /// This method to run get advert detail by descending, It is going to work with O(6) without Linq expression
        /// </summary>
        /// <param name="pageNumber">pageNumber</param>
        /// <returns>It will return data result that includes list of advert</returns>
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<AdvertReadDto>> GetAllAdvertDetail(int pageNumber)
        {
            double latitude = CurrentUser.Latitude;
            double longitude = CurrentUser.Longitude;
            var data = _advertDal.GetAllAdvertDetail(pageNumber, latitude, longitude, CurrentUser.User.Id);
            return new SuccessDataResult<List<AdvertReadDto>>(data);
        }


        /// <summary>
        /// This method run to get an advert detail by Id. If you want to get an advert detail you can use this method.
        /// This method is going to run with O(4) without Linq expression
        /// </summary>
        /// <param name="id"></param>
        /// <returns>It will return a data result that includes an advert</returns>
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<AdvertReadDto> GetAdvertDetailById(int id)
        {
            var data = _advertDal.GetAdvertDetailById(id, CurrentUser.User.Id, CurrentUser.Latitude, CurrentUser.Longitude);
            return new SuccessDataResult<AdvertReadDto>(data, AdvertMessages.AdvertGet);


        }


        /// <summary>
        /// This method run to get all advert by distance from close to far. It is going work with O(4) without Linq expression
        /// </summary>
        /// <param name="pageNumber">pageNumber</param>
        /// <returns>It will return data result that includes adverts shorted by distance</returns>
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        public IDataResult<List<AdvertReadDto>> GetAllAdvertByDistance(int pageNumber)
        {
            double latitude = CurrentUser.Latitude;
            double longitude = CurrentUser.Longitude;
            var data = _advertDal.GetAllAdvertByDistance(latitude, longitude, CurrentUser.User.Id, pageNumber);
            return new SuccessDataResult<List<AdvertReadDto>>(data, AdvertMessages.AdvertGetAll);

        }

        /// <summary>
        /// This method run to get all advert belongs a user by user ID, It is going  to work with O(4) without Linq expression
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="pageNumber">pageNumber</param>
        /// <returns>It will return data result that includes list of advert</returns>
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<AdvertReadDto>> GetAdvertDetailByUserId(int userId, int pageNumber)
        {
            var data = _advertDal.GetAllAdvertDetailsByFilter(a => a.UserId == userId, CurrentUser.User.Id, CurrentUser.Latitude, CurrentUser.Longitude, pageNumber);
            return new SuccessDataResult<List<AdvertReadDto>>(data, AdvertMessages.AdvertGetAll);
        }

        /// <summary>
        /// This method run to get all advert. If you don't need to detail of advert you should prefer this method because of more efficient and faster
        /// This method is going to run with O(4)
        /// </summary>
        /// <returns>It will return data result that includes list of adverts</returns>
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<Advert>> GetAll()
        {
            var data = _advertDal.GetAll();
            return new SuccessDataResult<List<Advert>>(data, AdvertMessages.AdvertGetAll);

        }

        /// <summary>
        /// This method run to update advert case. For instance, has it been adopted ? has it been found ?
        /// This method is going to run with O(2) 
        /// </summary>
        /// <param name="advert">advert</param>
        /// <returns>It will return a result that includes message</returns>
        [SecuredOperation($"{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail", Priority = 4)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById", Priority = 5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter", Priority = 6)]
        [CacheRemoveAspect("IAdvertService.GetAll", Priority = 7)]
        public IResult UpdateAdvertCase(Advert advert)
        {
            _advertDal.Update(advert);
            return new SuccessResult(AdvertMessages.AdvertStatus);
        }

        /// <summary>
        /// This method run to update status. For instance, has it been pending  ? has it been active ? has it been deactivate
        /// It is going to work with O(2)
        /// It would be pending to be default
        /// </summary>
        /// <param name="advert">advert</param>
        /// <returns>It will return a result that includes message</returns>
        [SecuredOperation($"{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail", Priority = 4)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById", Priority = 5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter", Priority = 6)]
        [CacheRemoveAspect("IAdvertService.GetAll", Priority = 7)]
        public IResult UpdateStatus(Advert advert)
        {
            _advertDal.Update(advert);
            return new SuccessResult(AdvertMessages.AdvertStatus);
        }

        /// <summary>
        /// This method to run update an advert while the process it can update it's images
        /// This method is going to run with O(n)
        /// </summary>
        /// <param name="advert">advert</param>
        /// <param name="advertImage">advert</param>
        /// <param name="files">files</param>
        /// <param name="location">location</param>
        /// <returns>It will return a result that includes message</returns>
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AdvertValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail", Priority = 5)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById", Priority = 6)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter", Priority = 7)]
        [CacheRemoveAspect("IAdvertService.GetAll", Priority = 8)]
        public async Task<IResult> Update(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location)
        {

            var image = _imageService.GetByAdvertId(advert.Id);
            if (files is not null)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);

                for (var i = 0; i < files.Length; i++)
                {
                    if (image.Data is null)
                    {
                        var result = await UploadFile(files[i], advert.Id);
                        if (!result.Success)
                            return new ErrorDataResult<Advert>(null, AdvertMessages.AdvertAdvertFailed);
                    }
                    else if (i > image.Data.Count)
                    {
                        var result = await UploadFile(files[i], advert.Id);
                        if (!result.Success)
                            return new ErrorDataResult<Advert>(null, AdvertMessages.AdvertAdvertFailed);
                    }
                    else
                    {
                        var fileResult = await fileHelper.UpdateAsync(files[i], image.Data[i].ImagePath, image.Data[i].PublicId);
                        var result = _imageService.Update(new AdvertImage
                        {
                            ImagePath = fileResult.Message.Split("&&")[0],
                            PublicId = fileResult.Message.Split("&&")[1],
                            AdvertId = advert.Id,
                            Id = image.Data[i].Id
                        });
                        if (!result.Success)
                            return new ErrorResult(AdvertMessages.AdvertUpdateFailed);
                    }
                }
            }

            _ = location.Id != 0 || location != null ? _locationService.Update(location) : null;
            _advertDal.Update(advert);

            return new SuccessResult(AdvertMessages.AdvertUpdate);
        }

        /// <summary>
        /// This method to run filter advert by coming params from request.
        /// This method is going to run with O(n) without Linq expression.
        /// Linq expression will check each property of object that is to pair an ID in the database.
        /// For instance, advert category ID is to pair array which name is Advert Category ID in object (AdvertFilterDto) 
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="pageNumber">pageNumber</param>
        /// <returns>It will return a data result of list of adverts</returns>
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        // [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        // [CacheAspect(Priority = 4)]
        public IDataResult<List<AdvertReadDto>> GetAllAdvertDetailsByFilter(AdvertFilterDto filter, int pageNumber)
        {
            // create a linq expression for filters
            Expression<Func<Advert, bool>> filters = c => true;

            // get properties from filter object, I mean AdvertFilterDto which mapped by coming params from request
            var properties = filter.GetType().GetProperties();
            foreach (var property in properties)
            {
                // get value of each property
                var value = property.GetValue(filter, null);
                // is it type of integer array
                if (value?.GetType() == typeof(int[]) && value != null)
                {
                    int[] arrayInteger = (int[])value;
                    if (arrayInteger.Length > 0)
                        filters = (Expression<Func<Advert, bool>>)StartFilterInvokeMethod(filters, value, property);

                }
                else if (value?.GetType() == typeof(Gender[]) && value != null)
                {
                    Gender[] arrayGender = (Gender[])value;
                    if (arrayGender.Length > 0)
                        filters = (Expression<Func<Advert, bool>>)StartFilterInvokeMethod(filters, value, property);
                }
                else
                {
                    if (value is not null and int and not 0 && property.Name != "Distance")
                        filters = (Expression<Func<Advert, bool>>)StartFilterInvokeMethod(filters, value, property);
                }
            }
            var latitude = CurrentUser.Latitude;
            var longitude = CurrentUser.Longitude;
            var data = _advertDal.GetAllAdvertDetailsByFilter(filters, CurrentUser.User.Id, latitude, longitude, pageNumber, filter.Distance);
            return new SuccessDataResult<List<AdvertReadDto>>(data, AdvertMessages.AdvertGetAll);
        }

        /// <summary>
        /// This method to run upload file to cloud or local storage. It depends on what you want which storage
        /// It is going to run asynchronous and with O(4)
        /// </summary>
        /// <param name="file">file</param>
        /// <param name="advertId">advertId</param>
        /// <returns>It will return a result that includes message</returns>
        private async Task<IResult> UploadFile(IFormFile file, int advertId)
        {
            var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
            var fileResult = await fileHelper.UploadAsync(file);
            var result = _imageService.Add(new AdvertImage
            {
                ImagePath = fileResult.Message.Split("&&")[0],
                PublicId = fileResult.Message.Split("&&")[1],
                AdvertId = advertId
            });
            return result;
        }


        private object StartFilterInvokeMethod(Expression<Func<Advert, bool>> filters, object value, PropertyInfo property)
        {
            // object must include filters type of expression  and value, Value might be any type depend on situations
            object[] methodParams = { filters, value };

            // AdvertManager inherited Advert Filter class and AdvertFilter class inherited BaseFilterInvoke class 
            // Base filter class include GetInvokeMethod that is taking few properties, one of it is name of method that run
            // first parameter methodName it must includes property name of AdvertFilterDto and filter method's is going to work 
            return (Expression<Func<Advert, bool>>)GetInvokeMethod($"{property.Name}Condition", methodParams);
        }
    }
}