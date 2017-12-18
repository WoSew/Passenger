using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt {get; protected set;}

        private readonly Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        protected User()
        {
        }

        public User(string email, string username, string password, string salt)
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Username = username;
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
        public void SetEmail(string email)
        {
            if(!EmailRegex.IsMatch(email))
            {
                throw new Exception("Adress email is valid.");
            }

            Email = email.ToLowerInvariant();
            UpDate();
        }

        private void UpDate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}