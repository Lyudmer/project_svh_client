using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Application.Services;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Xml.Linq;




namespace ClientSVH.Endpoints
{
    public static  class PackagesEndpoints
    {
        public static IEndpointRouteBuilder MapPackagesEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("Packages");
            app.MapPost("loadfile {UserId:guid}", LoadFile);
            app.MapGet("GetHistory{Pid:int}", GetHistoryPkg);
            app.MapGet("GetPackage{Pid:int}", GetPkgId);
            app.MapDelete("DelPackage{Pid:int}", DeletePkg);
            app.MapPost("send {Pid:int}", SendPkgToServer);
            app.MapPost("sendDelPkg {Pid:int}", SendDelPkgToServer);
            app.MapGet("GetDocsPackage{Pid:int}", GetDocsPkg);
            return app;
        }
        private static async Task<IResult> LoadFile(LoadFileRequest request, PackagesServices pkgService, Guid UserId)
        {
            
            using (var fileStream = new FileStream(request.FileName, FileMode.Create))
            {
                
                fileStream.Position = 0;
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var resFile = reader.ReadToEnd();
                    await ((IPackagesServices)pkgService).LoadFile(UserId, resFile);
                }
            }
            
            return Results.Ok();
        }
        
        private static async Task<IResult> SendPkgToServer(int Pid, PackagesServices pkgService)
        {
            await ((IPackagesServices)pkgService).SendToServer(Pid);
            return Results.Ok();
        }
        private static async Task<IResult> SendDelPkgToServer(int Pid, PackagesServices pkgService)
        {
            await ((IPackagesServices)pkgService).SendDelPkgToServer(Pid);
            return Results.Ok();
        }
        private static async Task<IResult> GetHistoryPkg(int Pid, PackagesServices pkgService)
        {
            await ((IPackagesServices)pkgService).HistoriPkgByPid(Pid);
            return Results.Ok();
        }
        private static async Task<IResult> GetPkgId(int Pid, PackagesServices pkgService)
        {
            await ((IPackagesServices)pkgService).GetPkgId(Pid);
            return Results.Ok();
        }  
        private static async Task<IResult> DeletePkg(int Pid, PackagesServices pkgService)
        {
            await ((IPackagesServices)pkgService).DeletePkg(Pid);
            return Results.Ok();
        } 
        private static async Task<IResult> GetDocsPkg(int Pid, PackagesServices pkgService)
        {
            await ((IPackagesServices)pkgService).GetDocsPkg(Pid);
            return Results.Ok();
        }
    }
}
