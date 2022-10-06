using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Core.CrossCuttingConcers.Caching;

namespace Core.Aspets.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // Benzersiz key oluşturuyor.
            var arguments = invocation.Arguments.ToList(); // parametreleri geziyor(varsa).
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key); // Return değeri cache'de ki data olsun demek.
                return; // Metodu burada keser. Aşağıya inmez.
            }
            invocation.Proceed();
            //Cache ekle.
            //Hangi key ile ? key.
            //Hangi değeri ? invocation.ReturnValue(örneğin getall metodu product listesi döndürüyor.).
            //Ne kadar süre kalsın? _duration
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
            
        }
    }
}
