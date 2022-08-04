﻿using Core.Entity.Abstracts;

namespace Core.Entity.Concretes
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OperationClaimId { get; set; }
    }
}