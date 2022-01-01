﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface ITicketService 
    {
        IDataResult<Ticket> Add(Ticket ticket);

        IResult Update(Ticket ticket);

        IResult Delete(Ticket ticket);

        IDataResult<Ticket> Get(int id);

        IDataResult<List<Ticket>> GetAll();
    }
}
