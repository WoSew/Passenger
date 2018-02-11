using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
  public class UserService : IUserService
  { 
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);

            return _mapper.Map<UserDto>(user);
        }
        
        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(Guid uderId ,string email, string username, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetHash(password, salt);
            user = new User(uderId ,email, username, role, hash, salt);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if(user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            var hash = _encrypter.GetHash(password, user.Salt);

            if(user.Password == hash)
            {
                return;
            }

            throw new Exception("Invalid credentials.");

        }
    }
}