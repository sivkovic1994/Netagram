using Microsoft.EntityFrameworkCore;
using Netagram.PostService.API.Middleware;
using Netagram.PostService.Infrastructure;
using Netagram.PostService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register Infrastructure services (DbContext, PostService, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Global error handler middleware catches all unhandled exceptions, logs them, and returns a consistent JSON error response to the client.
// Add this middleware at the start of the pipeline to ensure all errors are handled in one place.
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Auto-apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PostDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsProduction())
    app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();