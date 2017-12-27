using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller // users/...
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")] //an argument called email and he's required
        public async Task<UserDto> GetAsync(string email)
            => await _userService.GetAsync(email);

        [HttpPost("")]
        public async Task Post([FromBody]CreateUser request) //[FromBody] - atrybut ten jest wmagany do tego by freamwork ASP net core wiedział, że musi przypisywać rządanie HTTP ktore mu wyslemy w postaci obiektu json dokladnie do tych danych
            => await _userService.RegisterAsync(request.Email, request.Password, request.Password);
          
    }
}
