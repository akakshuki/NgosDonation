using Domain.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebMvc.Configurations
{
    public class NinjectConfiguration : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectConfiguration(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // put bindings here

            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}