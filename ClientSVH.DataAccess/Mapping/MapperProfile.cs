using AutoMapper;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;

namespace ClientSVH.DataAccess.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DocumentEntity, Document>()
                .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                    return true;
                }
                ));
            CreateMap<PackageEntity, Package>();
                //.ForAllMembers(x => x.Condition((src, dest, prop) =>
                //{
                //    if (prop == null) return false;
                //    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                //    return true;
                //}
                //));
            CreateMap<StatusEntity, Status>()
                .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                    return true;
                }
                ));
            CreateMap<UserEntity, User>()
                .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                    return true;
                }
                ));  
            CreateMap<HistoryPkgEntity, HistoryPkg>()
                .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                    return true;
                }
                ));
        }
    }
}
