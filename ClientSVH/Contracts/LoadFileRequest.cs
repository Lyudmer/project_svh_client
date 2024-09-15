using System.ComponentModel.DataAnnotations;

namespace ClientSVH.Contracts
{
    public record LoadFileRequest(

      [Required]
        string FileName
      );
}
