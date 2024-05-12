using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bussiness.User
{
    public class Users 
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter user name")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage ="Please enter pasword")]
        public string Password { get; set; } = string.Empty;
    }
}
