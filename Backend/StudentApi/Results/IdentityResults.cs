namespace StudentApi.Results
{
    public class ProfileResult : Result
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string GsmCountryCode { get; set; }
    }

    public class CheckAccountResult : Result
    {
        public bool IsFound { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
    }

    public class RegisterResult : Result
    {
        public string Username { get; set; }
    }
}