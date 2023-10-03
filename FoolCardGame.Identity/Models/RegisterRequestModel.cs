using System.ComponentModel.DataAnnotations;

namespace FoolCardGame.Identity.Models;

public class RegisterRequestModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}