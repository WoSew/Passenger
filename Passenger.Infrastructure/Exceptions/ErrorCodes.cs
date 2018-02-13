namespace Passenger.Infrastructure.Exceptions
{
    public static class ErrorCodes
    {
        public static string EmailInUnse => "email_in_use";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidCredentials => "invalid_credentials";
        public static string DriverNotFound => "driver_not_found";
        public static string DriverAlreadyExisty => "driver_already_existy";
        public static string UserNotFound => "user_not_found";
    }
}