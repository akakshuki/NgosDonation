using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository;
using Ninject;

namespace WebSite.Configurations
{
    public class NinjectConfiguration : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectConfiguration(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();

        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }


        private void AddBindings()
        {
            // put bindings here

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

        }
    }
}