using Castle.DynamicProxy;
using Core.CustomExceptions;
using Core.Utilities.Business;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspect.RuleAspect
{
    public class RuleAspect : MethodInterception
    {
        private readonly Type _ruleType;
        public RuleAspect(Type ruleType)
        {
            if (!typeof(IRule).IsAssignableFrom(ruleType)) 
                throw new ArgumentException("This type is not IRule Type");

            _ruleType = ruleType;
            
        }

        protected override void OnBefore(IInvocation invocation)
        {
            IRule rule = (IRule)Activator.CreateInstance(_ruleType);
            var result = rule.Run();
            if (!result.Success)
                throw new RuleException(result.Message);
                
        }
    }
}
