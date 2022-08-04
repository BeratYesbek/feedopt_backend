using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class TokenManager : ITokenService
    {
        private readonly ITokenDal _tokenDal;
        public TokenManager(ITokenDal tokenDal)
        {
            _tokenDal = tokenDal;
        }

        [SecuredOperation($"{Role.User},{Role.Admin},{Role.SuperAdmin}")]
        public async Task<IDataResult<Token>> Add(Token token)
        {
            var result = await FindByTokenAndCurrentUser(token);
            if (result.Success)
            {
                return new SuccessDataResult<Token>(null,"Token has been already added");
            }
            token.UserId = CurrentUser.User.Id;
            var addedToken = await _tokenDal.AddAsync(token);
            return new SuccessDataResult<Token>(addedToken);
        }

        [SecuredOperation($"{Role.User},{Role.Admin},{Role.SuperAdmin}")]
        public async Task<IDataResult<Token>> GetByCurrentUser()
        {
            var data = await _tokenDal.GetAllAsync(t => t.UserId == CurrentUser.User.Id);
            return new SuccessDataResult<Token>(data.Last());
        }

        [SecuredOperation($"{Role.User},{Role.Admin},{Role.SuperAdmin}")]
        public async Task<IDataResult<Token>> FindByTokenAndCurrentUser(Token token)
        {
            var data = await _tokenDal.GetAsync(t => t.UserId == CurrentUser.User.Id && t.UserToken == token.UserToken);
            if (data != null)
            {
                return new SuccessDataResult<Token>(data);
            }
            return new ErrorDataResult<Token>(null, "Token has not been found");
        }
    }
}
