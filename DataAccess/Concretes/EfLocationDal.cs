﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfLocationDal : EfEntityRepositoryBase<Location, NervioDbContext>
    {
    }
}