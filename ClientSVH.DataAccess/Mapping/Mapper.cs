using AutoMapper;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;

namespace ClientSVH.DataAccess.Mapping
{
    public class Mapper: Profile
    {
        public Mapper() 
        {
            CreateMap<UserEntity,User>(MemberList.Destination);
            CreateMap<DocumentEntity, Document>(MemberList.Destination);
            CreateMap<PackageEntity, Package>(MemberList.Destination);
            CreateMap<StatusEntity, Status>(MemberList.Destination);
            CreateMap<HistoryPkgEntity, HistoryPkg>(MemberList.Destination);
        }
    }
}
