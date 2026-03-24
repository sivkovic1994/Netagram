using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Netagram.FeedService.API.Middleware;
using Netagram.FeedService.Application.Interfaces;
using Netagram.FeedService.Infrastructure.Clients;
using Netagram.FeedService.Infrastructure.Services;
using Polly;
using Polly.Extensions.Http;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IFeedService, FeedService>();

var userServiceUrl = builder.Configuration.GetValue<string>("ServiceUrls:UserService") ?? "https://localhost:7017/";
var postServiceUrl = builder.Configuration.GetValue<string>("ServiceUrls:PostService") ?? "https://localhost:7040/";

// This policy will handle transient HTTP errors (5xx status codes) and also 429 Too Many Requests.
// It will retry the request up to 3 times, with an exponential backoff strategy (2^retryAttempt seconds).
// This helps to improve resilience when calling external services that might be temporarily unavailable or rate-limited.
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
    HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => (int)msg.StatusCode == 429)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
{
    client.BaseAddress = new Uri(userServiceUrl);
})
.AddPolicyHandler(GetRetryPolicy());

builder.Services.AddHttpClient<IPostServiceClient, PostServiceClient>(client =>
{
    client.BaseAddress = new Uri(postServiceUrl);
})
.AddPolicyHandler(GetRetryPolicy());

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
    };
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsProduction())
    app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();