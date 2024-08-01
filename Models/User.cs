namespace CubitsApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public string OtpExpiry { get; set; }
        public string IsVerified { get; set; }
    }
    public class OtpRequest
    {
        public string Email { get; set; }
    }

    public class OtpVerification
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
