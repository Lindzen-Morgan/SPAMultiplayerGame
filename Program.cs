using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiplayerGame.Data;
using MultiplayerGame.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Loopback, 5001);
});

// Add DbContext
builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<GameContext>()
    .AddDefaultTokenProviders();

// Add Authentication and Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Fallback to index.html for all routes that are not handled by API or other middlewares
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
    {
        context.Request.Path = "/index.html";
        await next();
    }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
    endpoints.MapFallbackToFile("/index.html"); // This serves index.html for all unhandled routes
});

app.Run();
