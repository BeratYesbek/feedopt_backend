﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface ICategoryService
    {
        IDataResult<Category> Add(Category category);

        IResult Update(Category category);

        IResult Delete(Category category);

        IDataResult<Category> Get(int Id);

        IDataResult<List<Category>> GetAll();
    }
}