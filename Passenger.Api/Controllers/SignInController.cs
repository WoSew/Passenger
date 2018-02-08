using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Api.Controllers
{
    public class SignInController : ApiControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        protected SignInController(ICommandDispatcher commandDispatcher, IMemoryCache memoryCache) : base(commandDispatcher)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Post([FromBody]SignIn command)
        {
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);

            var jwt = _memoryCache.GetJwt(command.TokenId);

            return Json(jwt);
        }
    }
}