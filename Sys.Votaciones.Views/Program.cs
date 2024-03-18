using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(c =>
{
    c.LoginPath = new PathString("/Student/Login");
    c.AccessDeniedPath = new PathString("/Student/Login");
    c.ExpireTimeSpan = TimeSpan.FromHours(8);
    c.SlidingExpiration = true;
    c.Cookie.HttpOnly = true;
});

//var url = builder.Configuration["ApiUrl:url"] ?? "";

//builder.Services.AddHttpClient("CRMAPI", c =>
//{
//    c.BaseAddress = new Uri(url);
//});


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

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
