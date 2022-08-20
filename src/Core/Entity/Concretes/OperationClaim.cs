
using System.Collections.Generic;
using Core.Entity.Abstracts;

namespace Core.Entity.Concretes
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<UserOperationClaim> UserOperationClaims;
    }
}
