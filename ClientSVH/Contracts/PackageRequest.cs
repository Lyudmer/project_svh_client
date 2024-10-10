using System.ComponentModel.DataAnnotations;

namespace ClientSVH.Contracts
{
    public record PackageRequest
    (
        [Required]
        int Pid
     
    );
    
}
