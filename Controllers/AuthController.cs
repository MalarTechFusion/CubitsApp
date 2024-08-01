using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CubitsApp.Models;
using CubitsApp.Services;
using System.Net.Mail;
using System.Net;

namespace CubitsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly EmailService _emailService;
        private static Dictionary<string, string> OtpStore = new Dictionary<string, string>();

        public AuthController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-otp")]
        public IActionResult SendOtp(OtpRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required.");
            }

            var otp = new Random().Next(1000, 9999).ToString();

            OtpStore[request.Email] = otp;

            _emailService.SendEmail(request.Email, otp);

            return Ok("OTP sent to email.");
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp(OtpVerification verification)
        {
            if (string.IsNullOrEmpty(verification.Email) || string.IsNullOrEmpty(verification.Otp))
            {
                return BadRequest("Email and OTP are required.");
            }

            if (OtpStore.TryGetValue(verification.Email, out var storedOtp))
            {
                if (storedOtp == verification.Otp)
                {
                    return Ok("OTP verified successfully.");
                }
            }

            return Unauthorized("Invalid OTP.");
        }
    }

}
