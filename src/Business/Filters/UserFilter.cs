using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Entity.Concretes;

namespace Business.Filters;

public class UserFilter : BaseFilterInvoke
{
    protected Expression<Func<User, bool>> StatusCondition(Expression<Func<User, bool>> filter, bool value)
    {
        return filter.And(t => t.Status == value);
    }

    protected Expression<Func<User, bool>> EmailConfirmedCondition(Expression<Func<User, bool>> filter, bool value)
    {
        return filter.And(t => t.EmailConfirmed == true);
    }

    protected Expression<Func<User, bool>> OperationClaimIdCondition(Expression<Func<User, bool>> filter, int[] value)
    {
        return filter.And(t => t.UserOperationClaims.Any(a => value.Contains(a.OperationClaimId)));
    }
}