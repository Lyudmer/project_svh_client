using AutoMapper;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;

namespace ClientSVH.DataAccess.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DocumentEntity, Document>().ReverseMap();

            CreateMap<PackageEntity, Package>().ReverseMap();

            CreateMap<Package, PackageEntity>().ReverseMap();

            CreateMap<StatusEntity, Status>().ReverseMap();
                
            CreateMap<UserEntity, User>().ReverseMap();

            CreateMap<HistoryPkgEntity, HistoryPkg>().ReverseMap();
                
        }
    }
}
