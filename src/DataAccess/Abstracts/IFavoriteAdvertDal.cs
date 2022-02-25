using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IFavoriteAdvertDal : IEntityRepository<FavoriteAdvert>
    {
        public List<FavoriteAdvertReadDto> GetAllDetailByFilter(Expression<Func<FavoriteAdvert,bool>> filter);
    }
}