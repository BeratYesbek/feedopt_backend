using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class SupportFile : IEntity
    {
        public int Id { get; set; }

        public string FileUrl { get; set; }

        public string PublicId { get; set; }

        public int TicketId { get; set; }

        public SupportFile()
        {

        }

        public SupportFile(int id,string fileUrl,string publicId,int ticketId):this(fileUrl, publicId, ticketId)
        {
            Id = id;
        }

        public SupportFile(string fileUrl, string publicId, int ticketId)
        {
            FileUrl = fileUrl;
            PublicId = publicId;
            TicketId = ticketId;
        }

    }
}
