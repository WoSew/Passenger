namespace Passenger.Core.Domain
{
    public static class ErrorCodes
    {
        //User
        public static string InvalidUsername => "invalid_username";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";

        //Vehicle
        public static string InvalidName => "invalid_name";      
        public static string InvalidNumberOfSeats => "invalid_number_of_seats";
        
        //Node
        public static string InvalidAdress => "invalid_adress";  
        public static string InvalidLongitude => "invalid_longitude";
        public static string InvalidLatitude => "invalid_latitude";

        //Route
        public static string NameAlreadyExist => "name_already_existy";
        public static string RouteNotFound => "route_not_found";
        public static string InvalidDistance => "invalid_distance";



    }
}