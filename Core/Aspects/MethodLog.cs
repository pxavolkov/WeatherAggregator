using System;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;

namespace WeatherAggregator.Core.Aspects
{
    [Serializable]
    public class MethodLog : OnMethodBoundaryAspect
    {
        private readonly Type _target;

        public MethodLog(Type target = null)
        {
            _target = target;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!args.Method.IsSpecialName) //Skip constructor, properties
            {
                var signature = args.Method.GetParameters();
                var methodName = args.Method.DeclaringType + "." + args.Method.Name;
                var arguments = args.Arguments.Select((e, i) => new { Name = signature[i].Name, Value = e });
                Logger.Info("Entering method {0} with parameters {1}", methodName, arguments);
            }
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Logger.Error(args.Exception);
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            bool result = false;

            if (_target != null)
            {
                result = _target.IsAssignableFrom(method.DeclaringType);
            }

            return result;
        }
    }
}
