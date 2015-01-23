using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ServiceStack.Configuration;

namespace WebApplication4
{
    public class WindsorContainerAdapter : IContainerAdapter, IDisposable
    {
        private readonly IWindsorContainer _container;

        public WindsorContainerAdapter()
        {
            _container = new WindsorContainer().Install(FromAssembly.InThisApplication(),
                FromAssembly.InDirectory(new ApplicationAssemblyFilter()));
        }

        public T TryResolve<T>()
        {
            if (_container.Kernel.HasComponent(typeof(T)))
            {
                return (T)_container.Resolve(typeof(T));
            }

            return default(T);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Dispose()
        {
            _container.Dispose();
        } 
    }

    public class ApplicationAssemblyFilter : AssemblyFilter
    {
        public ApplicationAssemblyFilter() 
            : base(AppDomain.CurrentDomain.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".*.dll"){}
    }
}