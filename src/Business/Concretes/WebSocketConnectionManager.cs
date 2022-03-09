using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    internal class WebSocketConnectionManager : IWebSocketConnectionService
    {
        public IDataResult<WebSocketConnection> Add(WebSocketConnection connection)
        {
            throw new NotImplementedException();
        }

        public IResult Update(WebSocketConnection connection)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(WebSocketConnection connection)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<WebSocketConnection>> GetByUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
