using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Users
{
    public class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _memoryCache;
        private readonly IHandler _handler;

        public SignInHandler(IHandler handler, IUserService userService, IJwtHandler jwtHandler, IMemoryCache memoryCache)
        {
            _handler = handler;
            _userService = userService;
            _jwtHandler = jwtHandler;
            _memoryCache = memoryCache;
        }

        public async Task HandleAsync(SignIn command)
            => await _handler
                .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
                .Next()
                .Run(async () => 
                {
                    var user = await _userService.GetAsync(command.Email);
                    var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

                    _memoryCache.SetJwt(command.TokenId, jwt);
                })
                .ExecuteAsync();


        // public async Task HandleAsync(SignIn command)
        // {
        //     await _userService.LoginAsync(command.Email, command.Password);
        //     var user = await _userService.GetAsync(command.Email);
        //     var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

        //     _memoryCache.SetJwt(command.TokenId, jwt);
        // }
    }
}