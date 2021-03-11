using AppDbContext.Enums;

namespace TeacherApi.Results
{
    public class LoginResult : Result
    {
        public LoginEnumResult ResultCode { get; set; }

        public string ResultText { get; set; }

        public string Token { get; set; }
    }
}