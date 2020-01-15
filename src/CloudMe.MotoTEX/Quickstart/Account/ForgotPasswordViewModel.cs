using System.ComponentModel.DataAnnotations;

namespace CloudMe.MotoTEX.Quickstart.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
