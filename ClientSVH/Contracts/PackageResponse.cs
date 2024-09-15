using ClientSVH.DataAccess.Entities;

namespace ClientSVH.Contracts
{
    public record PackageResponse
    (
        int Id,
        Guid UserId,
        DateTime CreateDate, 
        DateTime ModifyDate,
        Guid UUID,
        int StatusId
    );
}
