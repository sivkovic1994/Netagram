using Microsoft.EntityFrameworkCore;
using Netagram.PostService.Infrastructure;
using Netagram.PostService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register Infrastructure services (DbContext, PostService, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

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