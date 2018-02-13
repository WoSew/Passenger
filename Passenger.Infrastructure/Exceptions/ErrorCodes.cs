namespace Passenger.Infrastructure.Exceptions
{
    public static class ErrorCodes
    {
        //UserService
        public static string EmailInUnse => "email_in_use";
        public static string InvalidCredentials => "invalid_credentials";
        public static string DriverNotFound => "driver_not_found";
        public static string DriverAlreadyExisty => "driver_already_existy";
        public static string UserNotFound => "user_not_found";

        //VehicleProvider
        public static string BrandNotAvaliable => "brand_not_avaliable";
        public static string VehicleNotAvaliable => "vehicle_not_avaliable";
    }
}