using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

namespace Passenger.Api.Controllers
{
        public class UsersController : ApiControllerBase // users/...
        {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, GeneralSettings settings) : base(commandDispatcher)
        {
            _userService = userService;           
        }

        [Authorize(Policy = "admin")]
        [HttpGet("{email}")] //an argument called email and he's required
        public async Task<IActionResult> Get(string email)
        {
            var user =  await _userService.GetAsync(email);
            if(user == null)
            {
                return NotFound(); // HTTP code 404
            }

            return Json(user);
        }
            
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command) //[FromBody] - atrybut ten jest wmagany do tego by freamwork ASP net core wiedział, że musi przypisywać rządanie HTTP ktore mu wyslemy w postaci obiektu json dokladnie do tych danych
        {
            await CommandDispatcher.DispatchAsync(command);
            
            return Created($"users/{command.Email}", new object()); // HTTP code 201 + location: user/user10@email.com
        }
            
          
    }
}
