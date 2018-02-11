using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Drivers
{
    public class UpdateDriverHandler : ICommandHandler<UpdateDriver>
    {
        private readonly IDriverService _driverService;

        public UpdateDriverHandler(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public async Task HandleAsync(UpdateDriver command)
        {
            await _driverService.SetVehicleAsync(command.UserId,command.Vehicle.Brand, command.Vehicle.Name);
        }
    }
}