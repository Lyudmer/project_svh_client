using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Application.Services;

using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.DataAccess;
using ClientSVH.DataAccess.Repositories;
using ClientSVH.DocsBodyCore.Abstraction;
using ClientSVH.DocsBodyCore.Repositories;
using ClientSVH.DocsBodyDataAccess;
using ClientSVH.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddControllers();

services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IUsersService, UsersService>();

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddScoped<IPackagesServices, PackagesServices>();
services.AddScoped<IPackagesRepository, PackagesRepository>();
services.AddScoped<IDocumentsServices, DocumentsServices>();
services.AddScoped<IDocumentsRepository, DocumentsRepository>();
services.AddScoped<IDocRecordServices, DocRecordServices>();
services.AddScoped<IDocRecordRepository, DocRecordRepository>();

services.AddAutoMapper(typeof(IUsersService).Assembly);
services.AddAutoMapper(typeof(IPackagesServices).Assembly);
services.AddAutoMapper(typeof(IDocumentsServices).Assembly);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
//postgresql db
builder.Services.AddDbContext<ClientSVHDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(ClientSVHDbContext)));
    }
    );
//mongodb
builder.Services.Configure<DocsBodyDBConnectionSettings>(
builder.Configuration.GetSection("MongoDBContext"));
builder.Services.AddSingleton<DocRecordServices>();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseCors(x =>
//{
//    x.WithHeaders().AllowAnyHeader();
//    x.WithOrigins("http://localhost:3000");
//    x.WithMethods().AllowAnyMethod();
//});
app.Run();
