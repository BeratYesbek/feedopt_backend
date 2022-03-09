using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class WebSocketConnection : IEntity
    {
        public int Id { get; set; }

        public string ConnectionId { get; set; }

        public int UserId { get; set; }
    }
}
