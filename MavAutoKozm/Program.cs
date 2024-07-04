using MavAutoKozm.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<MavAutoKozmDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Itt regisztr�ljuk be a saj�t repository objektumunkat a saj�t interface-�nk�n kereszt�l, amit a controller
//l�trej�vetelekor p�ld�nyos�t az alkalmaz�s.
builder.Services.AddScoped<IMavAutoKozmRepository, MavAutoKozmRepository>();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

//ha itt "true" szerepel akkor nek�nk kell k�zzel elfogadni a reg ut�n az �j "User-t" a: dbo.AspNetUsers/EmailConfirmed mez�ben
//m�sik lehet�s�g egy email-es vagy sms-es aktiv�l�s ut�n a program �rja �t "true"-ra
//ha itt "false" szerepel ellen�rz�s n�lk�l reg ut�n be lehet jelentkezni
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.UseSession();
app.Run();