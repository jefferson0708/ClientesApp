using ClientesApp.Infra.Data.SqlServer.Extensions;
using ClienteApp.Domain.Extensions;
using ClientesApp.Application.Extensions;
using ClienteApp.Api.Extensions;
using ClientesApp.API.Middlewares;
using ClientesApp.Infra.Data.MongoDB.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(map => { map.LowercaseUrls = true; });
builder.Services.AddSwaggerConfig();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();
builder.Services.AddMongoDb(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseMiddleware<NotFoundExceptionMiddleware>();
app.UseSwaggerConfig();
app.UseAuthorization();
app.MapControllers();

app.Run();

