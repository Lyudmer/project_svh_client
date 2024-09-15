using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Application.Services;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;




namespace ClientSVH.Endpoints
{
    public static  class PackagesEndpoints
    {
        public static IEndpointRouteBuilder MapPackagesEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("Packages").RequireAuthorization();
            app.MapPost("loadfile {UserId:guid}", LoadFile);
        //    app.MapGet("{id:guid}", GetPkgUser);
       //     app.MapGet("{id:int}", GetPkgId);
       //     app.MapPut("{id:int}", UpdatePkg);
       //     app.MapDelete("{id:int}", DeletePkg);
       //     app.MapPost("send", SendPkg);
            return app;
        }
        private static async Task<IResult> LoadFile(LoadFileRequest request, PackagesServices pkgService, Guid UserId)
        {
            await ((IPackagesServices)pkgService).LoadFile(UserId,request.FileName);
            return Results.Ok();
        }
        //private static async Task<IResult> GetPkgUser(PackageResponse respons, PackagesServices pkgService)
        //{
        //    await ((IPackagesService)pkgService).OpenPkg();
        //    return Results.Ok();
        //}
        //private static async Task<IResult> SendPkg(PackageResponse respons, PackagesServices pkgService)
        //{
        //    await ((IPackagesService)pkgService).SendPkg();
        //    return Results.Ok();
        //}
    }
}
