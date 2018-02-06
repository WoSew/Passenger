using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class DriversController : ApiControllerBase
    {

        private readonly IDriverService _driverService;
        private readonly IUserService _userService;

        public DriversController(IDriverService driverService, ICommandDispatcher commandDispatcher, IUserService userService) 
            : base(commandDispatcher)
        {
            _driverService = driverService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _driverService.BrowseAsync();
            if(drivers == null)
            {
                return NotFound(); //404
            }

            return Json(drivers);
        }

        [HttpPost]
        public async Task<IActionResult> Put([FromBody]CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);
            
            return Created($"{command.UserId}", new object()); //HTTP code 201
        }


    }
}