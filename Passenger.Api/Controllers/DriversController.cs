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

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if(user == null)
            {
                return NotFound(); //404
            }

            var driver = await _driverService.GetAsync(user.Id);
            if(driver == null)
            {
                return NotFound(); //404
            }

            return Json(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Put([FromBody]CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);
            
            return Created($"{command.UserId}", new object()); //HTTP code 201
        }


    }
}