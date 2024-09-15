using System.ComponentModel.DataAnnotations;

namespace ClientSVH.Contracts
{
    public record RegisterUserRequest(
         string username,
      [Required]
      [EmailAddress]
        string email,
      [Required]
        string passwordHash
      );
}
