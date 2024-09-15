using System.ComponentModel.DataAnnotations;

namespace ClientSVH.Contracts
{
    public record LoginUserRequest
    (
     [Required]
        string email,
     [Required]
        string passwordHash
    );
}
