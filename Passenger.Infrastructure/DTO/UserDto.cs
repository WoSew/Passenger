using System;

namespace Passenger.Infrastructure.DTO
{
    public class UserDto //create usert DTO (data transfer object), will map models from domain to flat model that have only the data we want. We do not want to know in the API about the existence of a domain
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}