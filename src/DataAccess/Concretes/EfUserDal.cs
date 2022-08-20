using System.Collections.Generic;
using System.Linq;
using Core.DataAccess;
using Core.Entity.Concretes;
using DataAccess.Abstracts;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfUserDal : EfEntityRepositoryBase<User, AppDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new AppDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join UserOperationClaim in context.UserOperationClaims
                        on operationClaim.Id equals UserOperationClaim.OperationClaimId
                    where UserOperationClaim.UserId == user.Id
                    select new OperationClaim {Id = operationClaim.Id, Name = operationClaim.Name};

                return result.ToList();
            }
        }

        public List<UserDto> GetUserDetails()
        {
            using (var context = new AppDbContext())
            {
                var result = from user in context.Users
                    join userOperationClaim in context.UserOperationClaims on user.Id equals userOperationClaim.UserId
                    select new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FullName = user.FullName,
                        ImagePath = user.ImagePath,
                        PreferredLanguage = user.PreferredLanguage,
                        EmailConfirmed = user.EmailConfirmed,
                        Roles = string.Join(" ",(from operationClaim in context.OperationClaims where operationClaim.Id == userOperationClaim.OperationClaimId select operationClaim.Name).ToArray())
                        
                    };
                return result.ToList();
            }
        }
    }
}