using System.ComponentModel.DataAnnotations;

namespace Weathery.API.Models.Identity
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
