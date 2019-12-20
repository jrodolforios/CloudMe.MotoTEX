using System.ComponentModel.DataAnnotations;

namespace CloudMe.ToDeTaxi.Quickstart.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
