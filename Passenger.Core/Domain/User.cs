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
        private readonly Regex UsernameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        protected User()
        {
        }

        public User(string email, string username, string password, string salt)
        {
            Id = Guid.NewGuid();
            SetEmail(email);        //Email = email.ToLowerInvariant(); 
            SetUsername(username);  //Username = username;
            SetPassword(password);  //Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
        public void SetEmail(string email)
        {
            if(!EmailRegex.IsMatch(email))
            {
                throw new Exception("The email address is incorrect.");
            }

            Email = email.ToLowerInvariant();
            UpDate();
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password is incorrect.");
            }

            if(password.Length < 3)
            {
                throw new Exception("Password is too short. Password length must be greater than 3 characters.");
            }

            if(password.Length > 30)
            {
                throw new Exception("Password is too logn. Password must be shorter than 30 characters.");
            }
            
            if(Password == password)
            {
                return;
            }

            Password = password;
            UpDate();
        }
        public void SetUsername(string username)
        {
            if(!UsernameRegex.IsMatch(username))
            {
                throw new Exception("Username is incorrect.");
            }
            Username = username;
            UpDate();
        }
        private void UpDate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}