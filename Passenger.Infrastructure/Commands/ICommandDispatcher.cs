using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands
{
    public interface ICommandDispatcher //odpowiedzialny za to, że dostanie na wejscie jakas komende 
    {
         Task DispatchAsync<T>(T command) where T : ICommand;
    }
}