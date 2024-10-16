using Group06_Project.Domain.Interfaces;
using Group06_Project.Infrastructure.Configurations;
using Group06_Project.Infrastructure.Data;
using Group06_Project.Infrastructure.RealTime;
using Group06_Project.Infrastructure.Redis;
using Group06_Project.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConStr));
builder.Services.AddRedisCache(builder.Configuration);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddIdentitySetup(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.AddRazorPages(options => { options.Conventions.AuthorizeAreaFolder("Admin", "/", "AdminOnly"); });
builder.Services.AddSignalR();
var app = builder.Build();
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
dbInitializer.Initialize();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<SignalRHub>("/signalr");
app.Run();