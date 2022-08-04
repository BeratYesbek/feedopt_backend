using System;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Message { get; set; }

        public bool IsSeen { get; set; } = false;

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}

