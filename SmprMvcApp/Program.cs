//buras� istek hatt� (request pipeline)'n� olu�turur. Globalde genellikle bu bir boru hatt�na benzetilir.
//App' ilk istek at�ld���nda iste�e binaen a�a��daki Middleware(ara yaz�l�m) yap�lar� devreye girer.

using Microsoft.EntityFrameworkCore;
using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Concrete;
using SmprMvcApp.DAL.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SmprMvcApp.Common;

var builder = WebApplication.CreateBuilder(args);//Bu sat�r, WebApplication s�n�f�n�n bir �rne�ini olu�turur ve uygulamay� ba�latmak i�in yap�land�rma i�lemlerine ba�lar. CreateBuilder(args) metodu, uygulaman�n yap�land�rmas�n� ve ba��ml�l�klar�n� (dependencies) ayarlamak i�in kullan�l�r.

//NOT:Eskiden iki dosya kullan�l�rd�. Program.cs ve Startup.cs. Daha sonra ikisi Program.cs te birle�tirildi.

// Add services to the container.
builder.Services.AddControllersWithViews();//Bu sat�r, Dependency Injection (ba��ml�l�k enjeksiyonu) sistemi arac�l���yla MVC mimarisi i�in gerekli servisleri ekler. AddControllersWithViews(), uygulaman�n hem Controller s�n�flar�n� (yani i� mant���n� y�neten s�n�flar) hem de View (g�r�n�m) dosyalar�n� (UI) kullanmas�n� sa�lar.

//EF Core kullanarak veritaban�na ba�lan. Burdaki i�lemin ad� DI'dir.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages();

//Scoped ya�am s�resi, ayn� HTTP iste�i boyunca ayn� nesnenin yeniden kullan�lmas�n� sa�lar.
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
//builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
//builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

var app = builder.Build();//Buras�, uygulaman�n yap�land�rmas�n� tamamlay�p uygulamay� ba�lat�r. Build() metodu, uygulaman�n �al��maya haz�r hale gelmesini sa�lar ve app nesnesi �zerinden yap�land�rma i�lemlerine devam edilir.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())//Bu ko�ul, uygulaman�n �al��ma ortam�n� kontrol eder. E�er uygulama Development (Geli�tirme) ortam�nda de�ilse (�rne�in, Production veya Staging ortam�nda ise), hata y�netimi i�in UseExceptionHandler ve UseHsts gibi y�ntemler uygulan�r. Bu ko�ul, geli�mi� hata y�netimi ve g�venlik ayarlar�n� �retim ortam�nda etkinle�tirir.
{
    app.UseExceptionHandler("/Home/Error");//Bu sat�r, bir hata durumunda /Home/Error adresine y�nlendirme yapar. Bu sayede kullan�c�lar hata ekran�n� g�r�r ve daha g�venli bir hata y�netimi sa�lan�r.
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();//HTTP Strict Transport Security (HSTS) protokol�n� etkinle�tirir ve uygulaman�n yaln�zca HTTPS �zerinden �al��mas�n� sa�lar. Varsay�lan olarak, HSTS de�eri 30 g�nd�r ve bu s�re, uygulaman�n g�venli�ini sa�lamak i�in �retim ortam�nda art�r�labilir.
}

app.UseHttpsRedirection();//Bu sat�r, gelen HTTP isteklerini HTTPS isteklerine y�nlendirir. Bu, uygulaman�n g�venli ba�lant� �zerinden �al��mas�n� sa�lar.
app.UseStaticFiles();//Bu sat�r, uygulaman�n statik dosyalar�n� (CSS, JavaScript, resim vb.) sunmas�na izin verir. wwwroot klas�r�ndeki dosyalar bu �ekilde eri�ilebilir hale gelir.

app.UseRouting();//Bu sat�r, ASP.NET Core uygulamas�n�n URL desenlerini (routes) tan�mas�na ve i�lemesine olanak tan�r. Uygulama i�indeki y�nlendirmeleri ayarlamak i�in gereklidir. Ayn� zamanda buras� y�nlendirme (Routing) Middleware'dir.

//ASP.NET Core, middleware sıralamasına önem verir. Middleware'ler sırasıyla tetiklenir, bu yüzden önce UseAuthentication(doğrulama) sonra UseAuthorization(yetkilendirme) gelir. UseAuthentication() gelen isteği bir kullanıcıya bağlayarak kimlik doğrulama sağlar. UseAuthorization() isteğin yetkilendirme kurallarına uygun olup olmadığını kontrol eder.

app.UseAuthentication();//Kimlik doğrulama. kullanıcı adı veya şifre nin geçerli olup olmadığını kontrol eder. Eğer bunlar geçerliyse yetkilendime devreye girer. Giriş yapacak user'ın rolüne göre bazı sayfalara erişim sınırlaması getirir. Örneğin giriş yapan kişi customer ise sadece ürünleri ve ürünlerin detaylarını görebilir.
app.UseAuthorization();//Yetkilendirme. Bu satır, uygulama genelinde yetkilendirme kontrollerini etkinleştirir. Belirli sayfalara erişim izni olan kullanıcıları belirlemek için yetkilendirme işlemleri devreye alır.

app.MapRazorPages();
app.MapControllerRoute(//Bu k�s�m, uygulama i�in bir varsay�lan rota tan�mlar.
    name: "default",//Rota ad� olarak "default" verilmi�tir.
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");//Rota deseni olarak. Ayn� zamanda buras� bir endpoint olu�turur. controller=Home: Varsay�lan controller Home olarak ayarlanm��t�r. action=Index: Varsay�lan aksiyon Index olarak belirlenmi�tir. {id?}: id parametresi iste�e ba�l�d�r (? i�areti bu durumu belirtir) ve route i�inde bir de�ere sahip olmasa bile route �al��maya devam eder.

app.Run();//Bu sat�r, uygulaman�n �al��mas�n� ba�lat�r ve uygulama burada s�rekli olarak istekleri dinlemeye ba�lar.