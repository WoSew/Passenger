using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        
        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }
		public async Task<User> GetAsync(Guid userId)
		    => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);

		public async Task<User> GetAsync(string email)
		    => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

		public async Task<IEnumerable<User>> GetAllAsync()
            => await Users.AsQueryable().ToListAsync();

        public async Task<IEnumerable<Guid>> GetAllIdsAsync()
        {
            await Task.CompletedTask;
            return null;
        }

        public async Task<Guid> GetGuidAsync(string email)
        {
            var user = await GetAsync(email);
            return user.Id;
        }

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task RemoveAsync(Guid id)
            => await Users.DeleteOneAsync( x => x.Id == id );

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync( x => x.Id == user.Id, user); //nadpisanie

        public Task<IEnumerable<User>> BrowseAsync()
        {
            throw new NotImplementedException();
        }
    }
}