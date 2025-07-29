using System.ComponentModel.DataAnnotations;


namespace AuthenticationAPI.Application.DTOs
{
    public record AppUserDTO
    (
        int id, 
        [Required] string Name,
        [Required] string Address,
        [Required] string PhoneNumber,
        [Required,EmailAddress] string Email,
        [Required] string Password,
        [Required] string Role
        );
}
