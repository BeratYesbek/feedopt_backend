﻿using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.concretes;

namespace DataAccess.Abstracts
{
    public interface IAdoptionNoticeDal : IEntityRepository<AdoptionNotice>
    {
    }
}