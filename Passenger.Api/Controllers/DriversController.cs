using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Api.Controllers
{
    public class DriversController : ApiControllerBase
    {
        public DriversController(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher)
        {
          
        }
               
        [HttpPost]
        public async Task<IActionResult> Put([FromBody]CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);
            
            return NoContent(); // status code 204 - operacja sie powiodla i nie zwraca nic poza tym
        }
    }
}