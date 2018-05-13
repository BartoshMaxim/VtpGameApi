using System.ComponentModel.DataAnnotations;

namespace VtpGameApi.Models
{
    public class User
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}