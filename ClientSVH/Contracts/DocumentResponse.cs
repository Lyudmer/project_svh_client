using ClientSVH.DataAccess.Entities;

namespace ClientSVH.Contracts
{
    public record DocumentResponse
    (
        int Id,
        string Number,
        DateTime DocDate,
        string ModeCode,
        string DocType,
        int SizeDoc, 
        string Idmd5, 
        string IdSha256,
        DateTime CreateDate,
        DateTime ModifyDate,
        int Pid,
        Guid DocId
    );
}
