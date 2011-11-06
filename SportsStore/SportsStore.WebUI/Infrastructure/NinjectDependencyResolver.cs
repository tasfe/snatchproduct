using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System.Configuration;
using Ninject.Syntax;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
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
        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }
        public IKernel Kernel
        {
            get { return kernel; }
        }
        private void AddBindings()
        {
            // put additional bindings here
            Bind<IProductRepository>().To<EFProductRepository>();
            Bind<IAuthProvider>().To<FormsAuthProvider>();
            // create the email settings object
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(
                ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            Bind<IOrderProcessor>()
            .To<EmailOrderProcessor>()
            .WithConstructorArgument("settings", emailSettings);
        }
    }
}