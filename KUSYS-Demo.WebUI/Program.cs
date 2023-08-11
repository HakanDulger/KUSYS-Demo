using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Data.Concrete.EfCore;
using KUSYS_Demo.Data.Utilities;
using KUSYS_Demo.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using static KUSYS_Demo.Identity.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddMvc(options => { var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); options.Filters.Add(new AuthorizeFilter(policy)); })
     .AddNewtonsoftJson(options => {
         options.SerializerSettings.ContractResolver = new DefaultContractResolver();
         options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
         options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
     });


builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<KusysDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
b => b.MigrationsAssembly("KUSYS-Demo.Data").CommandTimeout(120).EnableStringComparisonTranslations(true)));

_ = builder.Services.AddTransient<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("IdentityConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("IdentityConnection")),

    b => b.CommandTimeout(120).EnableStringComparisonTranslations(true)
    ));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
})
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(12));
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
    options.LogoutPath = new PathString("/Account/Logout");
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminRolePolicy",
        policy => policy.RequireRole("Admin"));

    options.AddPolicy("UserRolePolicy",
        policy => policy.RequireRole("User"));

    options.AddPolicy("AdminUserPolicy",
               policy => policy.RequireAssertion(
                   context => context.User.IsInRole(RoleType.Admin.Description()) ||
                              context.User.IsInRole(RoleType.User.Description())));

});

var app = builder.Build();

app.UseSession();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Home/Error");
    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
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


