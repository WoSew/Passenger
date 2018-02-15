using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IHandler : IServices
    {
        IHandlerTask Run(Func<Task> run);
        IHandlerTaskRunner Validate(Func<Task> validate);
        Task ExecuteAllAsync();

    }
}