
using ClienSVH.XMLParser;
using ClientSVH.Application.Interfaces;
using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Application.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;

using ClientSVH.DataAccess;
using ClientSVH.DataAccess.Mapping;
using ClientSVH.DataAccess.Repositories;
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordDataAccess;
using ClientSVH.Extensions;
using ClientSVH.Infrastructure;
using ClientSVH.SendReceivServer;
using ClientSVH.SendReceivServer.Consumer;
using ClientSVH.SendReceivServer.Producer;
using ClientSVH.SendReceivServer.Settings;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;



var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var configuration = builder.Configuration;

services.AddApiAuthentication(configuration);
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


//postgresql db

services.AddDbContext<ClientSVHDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

//mongodb
services.Configure<Settings>(configuration.GetSection("MongoConnection"));


services.AddTransient<IMongoClient>(_ =>
{
    var connectionString = configuration.GetSection("MongoConnection:ConnectionString")?.Value;

    return new MongoClient(connectionString);
});
services.Configure<JwtOptions>(configuration.GetSection("JWT"));
services.AddTransient<IUsersService, UsersService>();
services.AddTransient<IPackagesServices, PackagesServices>();
services.AddTransient<IDocumentsServices, DocumentsServices>();
services.AddTransient<IHistoryPkgRepository, HistoryPkgRepository>();

services.AddTransient<IUsersRepository, UsersRepository>();
services.AddTransient<IPackagesRepository, PackagesRepository>();
services.AddTransient<IDocumentsRepository, DocumentsRepository>();
services.AddTransient<IDocRecordRepository, DocRecordRepository>();
services.AddTransient<IJwtProvider, JwtProvider>();
services.AddTransient<IPasswordHasher, PasswordHasher>();

services.AddTransient<ILoadFromFile, LoadFromFile>();
services.AddTransient<IRabbitMQBase, RabbitMQBase>();
services.AddTransient<IMessagePublisher, RabbitMQProducer>();
services.AddTransient<IRabbitMQConsumer, RabbitMQConsumer>();
services.AddTransient<ISendToServer, SendToServer>();
services.AddTransient<IReceivFromServer, ReceivFromServer>();

services.AddAutoMapper(typeof(MapperProfile));
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
