using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using PetApiRepository;
using PetApi.Services;

namespace PetApi
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(PetService<>))
                  .As(typeof(IPetService<>))
                  .InstancePerRequest();
                        
            builder.RegisterGeneric(typeof(PetRepository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}