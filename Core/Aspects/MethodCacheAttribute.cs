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
    public abstract class MethodCacheAttribute : BaseMethodInterceptor
    {
        internal static Func<ICacheRepository> CacheRepositoryFactory;

        protected abstract int Timeout { get; }

        protected MethodCacheAttribute(Type target = null)
            : base(target) { }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var cache = CacheRepositoryFactory();
            var key = BuildCacheKey(args.Method, args.Arguments);
            var cacheEntry = cache[key] as CacheEntry;
            object result;
            if (cacheEntry == null || (DateTime.Now - cacheEntry.DateCreated).TotalSeconds > Timeout)
            {
                Logger.Info("Key missed in cache: {0}", key);
                base.OnEntry(args);
                result = args.ReturnValue;
                cache[key] = new CacheEntry
                {
                    Value = args.ReturnValue,
                    DateCreated = DateTime.Now
                };
            }
            else
            {
                Logger.Info("Key found in cache: {0}", key);
                result = cacheEntry.Value;
            }
            args.ReturnValue = result;
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
        }
    }
}
