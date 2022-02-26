using System.Data;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class FavoriteAdvertValidator : AbstractValidator<FavoriteAdvert>
    {
        public FavoriteAdvertValidator()
        {
            RuleFor(f => f.AdvertId).NotNull().NotEmpty();
            RuleFor(f => f.UserId).NotNull().NotEmpty();
        }        
    }
}