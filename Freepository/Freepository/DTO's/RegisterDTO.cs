using System.ComponentModel.DataAnnotations;
namespace Freepository.DTO_s;

public class RegisterDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo es inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 carácteres")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Confirma tu contraseña")]
    [Compare("Password", ErrorMessage = "Las conraseñas no coinciden")]
    public string ConfirmPassword { get; set; }
    
    
}