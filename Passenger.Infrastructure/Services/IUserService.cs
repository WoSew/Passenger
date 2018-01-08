using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService : IServices
    {
        Task<UserDto> GetAsync(string email);
        Task RegisterAsync(string email, string username, string password, string role);
    }
}