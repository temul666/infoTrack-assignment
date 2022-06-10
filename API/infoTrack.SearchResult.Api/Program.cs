using infoTrack.SearchResult.Api.Middleware;
using infoTrack.SearchResult.Core.Repositories;
using infoTrack.SearchResult.Data;
using infoTrack.SearchResult.Services;
using infoTrack.SearchResult.Services.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IDbTran, DbTran>();

builder.Services.AddDbContext<ResultLogDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("infoTrack.SearchResult.Data")));

builder.Services.AddTransient<IResultLogService, ResultLogService>();

builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowReactApp",
                      policy =>
                      {
                          policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                      });
});

var app = builder.Build();

app.MapHealthChecks("/api/health");

app.CreateDatabase<ResultLogDbContext>();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.UseCors("AllowReactApp");

app.MapControllers();

app.Run();



public static class ExtensionMethods
{
    public static IHost CreateDatabase<T>(this IHost appHost) where T : DbContext
    {
        using (var scope = appHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Database creation failed!");
            }
        }
        return appHost;
    }
}