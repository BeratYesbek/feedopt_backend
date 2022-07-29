
using Core.Entity.Abstracts;

namespace Core.Entity.Concretes
{
    public enum ClientType
    {
        Ios,
        Android,
        Web
    }
    public class Token : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserToken { get; set; }
        public ClientType ClientType { get; set; }

    }
}
