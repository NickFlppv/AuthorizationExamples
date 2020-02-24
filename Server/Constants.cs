namespace Server
{
    
    //these constants have to be stored secretly
    public static class Constants
    {
        public const string Audience = "https://localhost:5001/";
        public const string Issuer = Audience;
        public const string Secret = "not_too_short_secret_otherwise_it_might_error";
    }
}