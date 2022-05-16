﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IUserLocationService
    {
        IDataResult<UserLocation> Add(UserLocation location);

        IResult Update(UserLocation location);

        IResult Delete(UserLocation location);

        IDataResult<UserLocation> Get(int id);

        IDataResult<UserLocation> GetById(int id);

        IDataResult<List<UserLocation>> GetAll();

        Task<IDataResult<UserLocation>> AddAsync(UserLocation userLocation);

    }
}