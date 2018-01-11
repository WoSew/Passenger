
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class DriversControllerTests : ControllerTestsBase
    {
        /*
        [Fact]
        public async Task get_user_by_correct_email_id_driver_should_not_exist()
        {
            var email = "user1@email.com";
            var user = await GetUserAsync(email);
            var driver = await GetDriverAsync(email);

            driver.Id.Should().NotBe(user.Id);
        }

        [Fact]
        public async Task given_unique_user_driver_should_be_created()
        {
            var command = new CreateDriver
            {
                
                
            };
        } 

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        private async Task<DriverDto> GetDriverAsync(string email)
        {
            var response = await Client.GetAsync($"drivers/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DriverDto>(responseString);
        }
        */
    }
}
 
