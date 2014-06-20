using System;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;

namespace WeatherAggregator.Core.Aspects
{
    [Serializable]
    public abstract class BaseMethodInterceptor : OnMethodBoundaryAspect
    {
        private readonly Type _target;

        protected BaseMethodInterceptor(Type target)
        {
            _target = target;
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            var result = !method.IsSpecialName; //Skip constructor, properties

            if (result && _target != null)
            {
                result = _target.IsAssignableFrom(method.DeclaringType);
                result &= _target.GetMethods().Select(m => m.Name).Contains(method.Name);
            }

            return result;
        }
    }
}
