using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ForgotPasswordModel
    {
        [Required,EmailAddress,Display(Name ="Registered Email Account")]
        public string Email { get; set; }

        public bool EmailSent { get; set; }
        public bool IsRegistered { get; set; }
    }
}
