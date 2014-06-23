using System;
using System.Linq;
using PostSharp.Aspects;

namespace WeatherAggregator.Core.Aspects
{
    [Serializable]
    public class MethodLogAttribute : BaseMethodInterceptor
    {
        public MethodLogAttribute(Type target = null)
            : base(target) { }

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
    }
}
