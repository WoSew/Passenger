using System;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Driver> GerOrFailAsync(this IDriverRepository repository, Guid userId)
        {
            var driver = await repository.GetAsync(userId);
            if(driver == null)
            {
                throw new Exception($"Selected driver with user id: '{userId}' was not found.");
            }
            return driver;
        }

        public static async Task<User> GerOrFailAsync(this IUserRepository repository, Guid userId)
        {
            var user = await repository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception($"Selected user with id: '{userId}' was not found.");
            }
            return user;
        }
    }
}