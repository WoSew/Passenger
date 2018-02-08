using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("drivers/routes")]
    public class DriverRouteController : ApiControllerBase
    {

        protected DriverRouteController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriverRoute command)
        {
            await DispatchAsync(command);
            
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var command = new DeleteDriverRoute
            {
                Name = name
            };
            await DispatchAsync(command);
            
            return NoContent();
        }
    }
}