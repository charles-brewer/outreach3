using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Identity.Client;
using outreach3.Data;

using System.Diagnostics;
using outreach3.Data.Ministries;
using Coravel;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();




builder.Services.AddIdentity<IdentityUser, IdentityRole>(

    options =>
    {
        options.Stores.MaxLengthForKeys = 128;
        options.SignIn.RequireConfirmedAccount = true;

    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();




builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/Churches");
    options.Conventions.AuthorizeFolder("/Admin");
    options.Conventions.AuthorizeFolder("/Members");
    options.Conventions.AuthorizeFolder("/Missions");
    options.Conventions.AuthorizeFolder("/Residents");
    options.Conventions.AuthorizeFolder("/Pages");
    options.Conventions.AuthorizeFolder("/Pages/Images");
    options.Conventions.AuthorizeFolder("/Shared");
    options.Conventions.AuthorizeFolder("/Visitations");
    

});

builder.Services.AddAuthentication();

builder.Services.AddScheduler();
builder.Services.AddTransient<SendWeeklyEmailReports>();

var app = builder.Build();


app.Services.UseScheduler(scheduler =>
{
   
   scheduler.Schedule<SendWeeklyEmailReports>().Weekly().Tuesday();
    
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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
app.UseAuthorization();
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

    
    IdentitySeedData.Initialize(context, userMgr, roleMgr).Wait();
}



// setup app's root folders
AppDomain.CurrentDomain.SetData("ContentRootPath", app.Environment.ContentRootPath);
AppDomain.CurrentDomain.SetData("WebRootPath", app.Environment.WebRootPath);
app.MapRazorPages();
app.UseStatusCodePagesWithRedirects("/errors/{0}");
app.UseStatusCodePagesWithReExecute("/errors/{0}");





app.Run();


