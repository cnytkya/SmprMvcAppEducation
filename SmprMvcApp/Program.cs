//burasý istek hattý (request pipeline)'ný oluþturur. Globalde genellikle bu bir boru hattýna benzetilir.
//App' ilk istek atýldýðýnda isteðe binaen aþaðýdaki Middleware(ara yazýlým) yapýlarý devreye girer.

using Microsoft.EntityFrameworkCore;
using SmprMvcApp.Data;

var builder = WebApplication.CreateBuilder(args);//Bu satýr, WebApplication sýnýfýnýn bir örneðini oluþturur ve uygulamayý baþlatmak için yapýlandýrma iþlemlerine baþlar. CreateBuilder(args) metodu, uygulamanýn yapýlandýrmasýný ve baðýmlýlýklarýný (dependencies) ayarlamak için kullanýlýr.

//NOT:Eskiden iki dosya kullanýlýrdý. Program.cs ve Startup.cs. Daha sonra ikisi Program.cs te birleþtirildi.

// Add services to the container.
builder.Services.AddControllersWithViews();//Bu satýr, Dependency Injection (baðýmlýlýk enjeksiyonu) sistemi aracýlýðýyla MVC mimarisi için gerekli servisleri ekler. AddControllersWithViews(), uygulamanýn hem Controller sýnýflarýný (yani iþ mantýðýný yöneten sýnýflar) hem de View (görünüm) dosyalarýný (UI) kullanmasýný saðlar.

//EF Core kullanarak veritabanýna baðlan. Burdaki iþlemin adý DI'dir.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();//Burasý, uygulamanýn yapýlandýrmasýný tamamlayýp uygulamayý baþlatýr. Build() metodu, uygulamanýn çalýþmaya hazýr hale gelmesini saðlar ve app nesnesi üzerinden yapýlandýrma iþlemlerine devam edilir.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())//Bu koþul, uygulamanýn çalýþma ortamýný kontrol eder. Eðer uygulama Development (Geliþtirme) ortamýnda deðilse (örneðin, Production veya Staging ortamýnda ise), hata yönetimi için UseExceptionHandler ve UseHsts gibi yöntemler uygulanýr. Bu koþul, geliþmiþ hata yönetimi ve güvenlik ayarlarýný üretim ortamýnda etkinleþtirir.
{
    app.UseExceptionHandler("/Home/Error");//Bu satýr, bir hata durumunda /Home/Error adresine yönlendirme yapar. Bu sayede kullanýcýlar hata ekranýný görür ve daha güvenli bir hata yönetimi saðlanýr.
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();//HTTP Strict Transport Security (HSTS) protokolünü etkinleþtirir ve uygulamanýn yalnýzca HTTPS üzerinden çalýþmasýný saðlar. Varsayýlan olarak, HSTS deðeri 30 gündür ve bu süre, uygulamanýn güvenliðini saðlamak için üretim ortamýnda artýrýlabilir.
}

app.UseHttpsRedirection();//Bu satýr, gelen HTTP isteklerini HTTPS isteklerine yönlendirir. Bu, uygulamanýn güvenli baðlantý üzerinden çalýþmasýný saðlar.
app.UseStaticFiles();//Bu satýr, uygulamanýn statik dosyalarýný (CSS, JavaScript, resim vb.) sunmasýna izin verir. wwwroot klasöründeki dosyalar bu þekilde eriþilebilir hale gelir.

app.UseRouting();//Bu satýr, ASP.NET Core uygulamasýnýn URL desenlerini (routes) tanýmasýna ve iþlemesine olanak tanýr. Uygulama içindeki yönlendirmeleri ayarlamak için gereklidir. Ayný zamanda burasý yönlendirme (Routing) Middleware'dir.

app.UseAuthorization();//Bu satýr, uygulama genelinde yetkilendirme kontrollerini etkinleþtirir. Belirli sayfalara eriþim izni olan kullanýcýlarý belirlemek için yetkilendirme iþlemlerini devreye alýr.

app.MapControllerRoute(//Bu kýsým, uygulama için bir varsayýlan rota tanýmlar.
    name: "default",//Rota adý olarak "default" verilmiþtir.
    pattern: "{controller=Home}/{action=Index}/{id?}");//Rota deseni olarak. Ayný zamanda burasý bir endpoint oluþturur. controller=Home: Varsayýlan controller Home olarak ayarlanmýþtýr. action=Index: Varsayýlan aksiyon Index olarak belirlenmiþtir. {id?}: id parametresi isteðe baðlýdýr (? iþareti bu durumu belirtir) ve route içinde bir deðere sahip olmasa bile route çalýþmaya devam eder.

app.Run();//Bu satýr, uygulamanýn çalýþmasýný baþlatýr ve uygulama burada sürekli olarak istekleri dinlemeye baþlar.