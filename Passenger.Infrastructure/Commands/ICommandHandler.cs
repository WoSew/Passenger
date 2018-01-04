using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand // odpowiedzialny za poprawny handling naszych komend  
    {
         Task HandleAsync(T command);
    }
}