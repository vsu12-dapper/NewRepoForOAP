using OAP_Active.App_Code;

var builder = WebApplication.CreateBuilder(args);

// Add configuration to DI container
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout duration
    options.Cookie.HttpOnly = true;  // Prevent JS access
    options.Cookie.IsEssential = true; // Essential cookie
});

builder.Services.AddHttpClient();
// Register GeneralMethod class as Scoped
builder.Services.AddScoped<GeneralMethod>();
builder.Services.AddSingleton<DCRS>();
var app = builder.Build();



// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();  // Ensure session is used

app.UseRouting();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "registration",
//    pattern: "{controller=REG}/{action=REG}/{id?}"
//);
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LogIn}/{action=Index}/{id?}"
);
app.MapControllers();

app.Run();
