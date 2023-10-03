using System.ComponentModel.DataAnnotations;

namespace FoolCardGame.Identity.Models;

public class LoginRequestModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}