using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
  public class InMemoryUserRepository : IUserRepository
  {
        private static ISet<User> _users = new HashSet<User>//ISet collection with unique elements
        {
            new User("user1@email.com", "user1", "secret", "user", "salt"),
            new User("user2@email.com", "user2", "secret", "user", "salt"),
            new User("user3@email.com", "user3", "secret", "user", "salt")
        };

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id)); // await Task.FromResult(...); dopisujemy tylko po to by kompilator nie pokazywal warrningow

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<Guid> GetGuidAsync(string email)
        {
            var user = await GetAsync(email);
            return user.Id;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }
        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Guid>> GetAllIdsAsync()
        {
            //TODO - GET Guid, dunno how but its important to go ahead..
            /*GuidAttribute usersAttribute = (GuidAttribute) Attribute.GetCustomAttribute(typeof(InMemoryUserRepository));

            IEnumerable<Guid> usersGuids = new Guid(usersAttribute.Value);  */       
            await Task.CompletedTask;
            return null;
        }
           
    }
}