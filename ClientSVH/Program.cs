using AutoMapper;
using ClienSVH.XMLParser;
using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Application.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;

using ClientSVH.DataAccess;
using ClientSVH.DataAccess.Repositories;
using ClientSVH.DocsBodyCore.Abstraction;
using ClientSVH.DocsBodyDataAccess;
using ClientSVH.DocsBodyDataAccess.Repositories;
using ClientSVH.Extensions;
using ClientSVH.Infrastructure;
using ClientSVH.SendServer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var configuration = builder.Configuration;
services.AddApiAuthentication(configuration);
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

//postgresql db
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
services.AddDbContext<ClientSVHDbContext>(options =>{options.UseNpgsql(connectionString);});

services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

services.AddTransient<IUsersService,UsersService>();
services.AddTransient<IPackagesServices,PackagesServices>();
//mongodb
services.Configure<DocsBodyDBConnectionSettings>(configuration.GetSection("MongoDBContext"));



services.AddTransient<IUsersRepository, UsersRepository>();
services.AddTransient<IPackagesRepository, PackagesRepository>();
services.AddTransient<IDocumentsRepository, DocumentsRepository>();
services.AddTransient<IDocRecordRepository, DocRecordRepository>();

services.AddTransient<ILoadFromFile, LoadFromFile>();
services.AddTransient<ISendToServer, SendToServer>();

services.AddTransient<IJwtProvider, JwtProvider>();
services.AddTransient<IPasswordHasher, PasswordHasher>();

services.AddAutoMapper(typeof(UsersService).Assembly);
services.AddAutoMapper(typeof(PackagesServices).Assembly);

services.AddHttpContextAccessor();

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
