using System.Reflection;
using Autofac;
using Passenger.Infrastructure.Commands;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly; //mamy nasze assembly (dll ze wszystkim z Infrastrukry pobrane za pomoca refleksji z dowolnej klasy w Infrastrucuture w tym przypadku CommandModule)

            builder.RegisterAssemblyTypes(assembly)
                    .AsClosedTypesOf(typeof(ICommandHandler<>))
                    .InstancePerLifetimeScope(); //bierzemy cale assembly (caly kod z Passenger.Infrastructure), autofac skanuje wszystkie klasy i interfejsy ktore sie znajduja w tym kodzie i znajduje ich implementacje czyli zrobi za nas np to: 

            // builder.RegisterType<CreateUserHandler>()
            //     .As<ICommandHandler<CreateUser>>()
            //     .InstancePerLifetimeScope();

            //a my szczesliwie nie musimy juz tego robic nawet jak tych implementacji bedzie 100

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}