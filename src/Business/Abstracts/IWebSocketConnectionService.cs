using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IWebSocketConnectionService
    {
        IDataResult<WebSocketConnection> Add(WebSocketConnection connection);

        IResult Update(WebSocketConnection connection);

        IResult Delete(WebSocketConnection connection);

        IDataResult<List<WebSocketConnection>> GetByUser(int userId);

        IDataResult<WebSocketConnection> GetByConnection(string connectionId);


    }
}
