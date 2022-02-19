using Project.Application.Business;
using Project.Application.IBusiness;
using Project.Application.Middleware;
using Project.Infrastructure.Context;
using Project.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Project.Domain.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("postgresConnectionString");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware(typeof(ErrorMiddleware));
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoint => endpoint.MapControllers());
app.MapControllers();

app.Run();