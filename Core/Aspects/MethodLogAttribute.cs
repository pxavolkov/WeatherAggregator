using System;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;

namespace WeatherAggregator.Core.Aspects
{
    [Serializable]
    public class MethodLogAttribute : OnMethodBoundaryAspect
    {
        private readonly Type _target;

        public MethodLogAttribute(Type target = null)
        {
            _target = target;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var signature = args.Method.GetParameters();
            var methodName = args.Method.DeclaringType + "." + args.Method.Name;
            var arguments = args.Arguments.Select((e, i) => new { Name = signature[i].Name, Value = e });
            Logger.Info("Entering method {0} with parameters {1}", methodName, arguments);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var methodName = args.Method.DeclaringType + "." + args.Method.Name;

            Logger.Info("Exited method {0} with result {1}", methodName, args.ReturnValue);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Logger.Error(args.Exception);
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
