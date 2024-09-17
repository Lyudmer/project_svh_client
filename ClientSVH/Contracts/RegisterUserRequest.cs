using System.ComponentModel.DataAnnotations;

namespace ClientSVH.Contracts
{
    public record RegisterUserRequest(
         string UserName,
      [Required]
      [EmailAddress]
        string Email,
      [Required]
        string PasswordHash
      );
}
