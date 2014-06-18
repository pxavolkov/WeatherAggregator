using System;
using System.Reflection;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Core.Aspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, AllowMultiple = false)]
    public class MethodCacheAttribute : MethodInterceptionAspect
    {
        internal static Func<ICacheRepository> CacheRepositoryFactory;
        internal static int CacheTimeoutSeconds;

        private readonly Type _target;

        public MethodCacheAttribute(Type target = null)
        {
            _target = target;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var cache = CacheRepositoryFactory();
            var key = BuildCacheKey(args.Method, args.Arguments);
            var cacheEntry = cache[key] as CacheEntry;
            object result;
            if (cacheEntry == null || cacheEntry.IsExpired())
            {
                result = args.Invoke(args.Arguments);
                cache[key] = new CacheEntry
                {
                    Value = result,
                    DateCreated = DateTime.Now
                };
            }
            else
            {
                result = cacheEntry.Value;
            }
            args.ReturnValue = result;
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            bool result = !method.IsSpecialName;

            if (result && _target != null)
            {
                result = _target.IsAssignableFrom(method.DeclaringType);
            }

            return result;
        }

        private string BuildCacheKey(MethodBase method, Arguments arguments)
        {
            return JsonConvert.SerializeObject(new
            {
                Class = method.DeclaringType.FullName,
                Method = method.Name,
                Arguments = arguments
            });
        }

        class CacheEntry
        {
            public object Value;
            public DateTime DateCreated;

            public bool IsExpired()
            {
                return (DateTime.Now - DateCreated).TotalSeconds > CacheTimeoutSeconds;
            }
        }
    }
}
