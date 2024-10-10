using System.ComponentModel.DataAnnotations;

namespace ClientSVH.Contracts
{
    public record DocRequest
    (
        [Required]
        int Id
     
    );
    
}
