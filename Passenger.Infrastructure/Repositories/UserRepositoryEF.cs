using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.EntityFramework;

namespace Passenger.Infrastructure.Repositories
{
    public class UserRepositoryEF : IUserRepository, ISqlRepository
    {
        private readonly PassengerContext _context;

        public UserRepositoryEF(PassengerContext context)
        {
            _context = context;
        }
    
        public async Task<User> GetAsync(Guid id)
            => await _context.Users.SingleOrDefaultAsync( x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await _context.Users.SingleOrDefaultAsync( x => x.Email == email);

        public async Task<IEnumerable<User>> BrowseAsync()
            => await _context.Users.ToListAsync();

        public Task<IEnumerable<Guid>> GetAllIdsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> GetGuidAsync(string email)
        {
            var user = await GetAsync(email);
            return user.Id;
        }

        public async Task AddAsync(User user) // musi być SAVE
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {   
            var user = await GetAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


    }
}