using System.Reflection;
using Autofac;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServicesModule)
                .GetTypeInfo()
                .Assembly; 

            builder.RegisterAssemblyTypes(assembly)
                    .Where( x=> x.IsAssignableTo<IServices>()) //które dziedziczą bądź implementuję określony typ (tu IServices)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

        }
    }
}