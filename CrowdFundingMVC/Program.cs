using CrowdFunding.Models;
using CrowdFunding.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CrowdFundingDB")));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IFundingPackageService, FundingPackageService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IVideoService, VideoService>();



builder.Services.AddIdentity<User, IdentityRole<int>>(
        options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
    .AddEntityFrameworkStores<CFContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
