using ClientSVH.Application.Services;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;


namespace ClientSVH.Endpoints
{
    public static class StatusEndpoints
    {
        public static IEndpointRouteBuilder MapStatusEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("AddStatus", AddSt);
            app.MapPost("DelStatus", DelSt);

            return app;
        }
        private static async Task<IResult> AddSt(StatusAddRequest request, StatusServices statusService)
        {
            await ((IStatusServices)statusService)
                .AddStatus(Status.Create(request.Id, request.StatusName, request.RunWf, request.MkRes, request.SendMess));
            return Results.Ok();
        }
        private static async Task<IResult> DelSt(StatusDelRequest request, StatusServices statusService)
        {
            await ((IStatusServices)statusService).DelStatus(request.Id);
            return Results.Ok();
        }
    }
}
