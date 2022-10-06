using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspets.Autofac.Caching
{
    // Data bozulduğu zaman(data eklenirse, silinirse,güncellenirse) çalışır.
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        //Hangi metodun üstüne yazıldıysa, o metod success olduğunda çalışacak.
        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
