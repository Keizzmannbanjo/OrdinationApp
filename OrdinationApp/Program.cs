using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdinationApp.Data;
using OrdinationApp.Models;
using OrdinationApp.Services.ModelServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
});


builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<TrackerUser, UserRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IMemberServices, MemberServices>();
builder.Services.AddScoped<IProvinceServices, ProvinceServices>();
builder.Services.AddScoped<IRankServices, RankServices>();
builder.Services.AddScoped<ICMCServices, CMCServices>();
builder.Services.AddScoped<IOrdinationBillServices, OrdinationBillServices>();
builder.Services.AddScoped<IPaymentRecordsServices, PaymentRecordsServices>();
builder.Services.ConfigureApplicationCookie(
    config =>
    {
        config.LoginPath = "/Admin/Login";
        config.AccessDeniedPath = "/Admin/AccessDenied";
    }
);

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
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
