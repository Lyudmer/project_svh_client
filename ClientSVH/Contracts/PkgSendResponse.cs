using System.ComponentModel.DataAnnotations;

namespace ClientSVH.Contracts
{
    public record PkgSendResponse
    (
        [Required]
        int Pid
     
    );
    
}
