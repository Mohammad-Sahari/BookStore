using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BookStore.Models
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password), Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password Confirmation")]
        [Compare("NewPassword", ErrorMessage ="Entered password does not match!")]
        public string ConfirmPassword { get; set; }

    }
}
