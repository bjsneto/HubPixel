using HubPixel.Application.MediaSource;
using HubPixel.Domain.Repository.MediaSource;
using HubPixel.Infrastructure.Data.Repositories;
using HubPixel.Infrastructure.Data; // Importa o namespace do DbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do DbContext e os repositórios
AddInfrastructure(builder.Services);

// Adiciona os serviços da camada de aplicação
AddApplication(builder.Services);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapStaticAssets();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger"; // Defina a rota do Swagger UI
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

static IServiceCollection AddInfrastructure(IServiceCollection services)
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ??
                           "Host=localhost;Database=db_hubpixel;Username=youruser;Password=yourpassword";

    services.AddDbContext<HubPixelDbContext>(options =>
        options.UseNpgsql(connectionString));

    services.AddTransient<IMediaSourceRepository, MediaSourceRepository>();

    return services;
}

static IServiceCollection AddApplication(IServiceCollection services)
{
    services.AddTransient<IM3u8ParserService, M3u8ParserService>();
    services.AddTransient<IMediaSourceService, MediaSourceService>();
    return services;
}