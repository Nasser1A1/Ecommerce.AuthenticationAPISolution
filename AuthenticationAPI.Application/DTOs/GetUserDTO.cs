using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.DTOs
{
    public record GetUserDTO
        (
            int id,
            [Required] string Name,
            [Required] string Address,
            [Required] string PhoneNumber,
            [Required, EmailAddress] string Email,
            [Required] string Role
            );
}
