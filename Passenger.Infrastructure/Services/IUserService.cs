using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService : IServices
    {
        Task<UserDto> GetAsync(string email);
        Task<UserDto> GetAsync(Guid userId);
        Task RegisterAsync(Guid userId,string email, string username, string password, string role);
        Task LoginAsync(string email, string password);
        Task <IEnumerable<UserDto>> BrowseAsync();
    }
}