using ClientSVH.Application.Services;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;




namespace ClientSVH.Endpoints
{
    public static  class DocumentsEndpoints
    {
        public static IEndpointRouteBuilder MapDocumentsEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("Documents");
            app.MapGet("GetDocument{Id:int}", GetDocId);
            app.MapDelete("GetDocRecord{Id:int}", GetDocRecId);
            app.MapGet("DeleteDoc{Id:int}", DelDocPkg);
            return app;
        }
        private static async Task<IResult> GetDocId(int Id, DocumentsServices docService)
        {
            await ((IDocumentsServices)docService).GetDocId(Id);
            return Results.Ok();
        }
        private static async Task<IResult> GetDocRecId(int Id, DocumentsServices docService)
        {
            await ((IDocumentsServices)docService).GetDocRecord(Id);
            return Results.Ok();
        }  
        private static async Task<IResult> DelDocPkg(int Id, DocumentsServices docService)
        {
            await ((IDocumentsServices)docService).DeleteDoc(Id);
            return Results.Ok();
        } 
       
    }
}
