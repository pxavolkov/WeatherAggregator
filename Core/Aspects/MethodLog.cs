using System;
using System.Linq;
using PostSharp.Aspects;

namespace WeatherAggregator.Core.Aspects
{
    [Serializable]
    public class MethodLog : OnMethodBoundaryAspect
    {
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
    }
}
