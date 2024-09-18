using ClientSVH.DataAccess.Entities;

namespace ClientSVH.Contracts
{
    public record PackageResponse
    (
        int Pid,
        Guid UserId,
        DateTime CreateDate, 
        DateTime ModifyDate,
        Guid UUID,
        int StatusId
    );
}
