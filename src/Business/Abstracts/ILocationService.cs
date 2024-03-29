﻿using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface ILocationService
    {
        IDataResult<Location> Add(Location location);

        IResult Update(Location location);

        IResult Delete(Location location);
    }
}
