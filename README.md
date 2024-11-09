# SmprMvcApp
SmartPro bünyesinde, eğitmenliğini yaptığım MVC projesi.
1-)İlk aşamadaki konular;
-MVC Tasarım deseni: Model,View,Controller çalışma presibi anlatıldı. Gerçek hayat örneğiyle mantığı kavrandı.
-Connection String ile veritabanına bağlanmak: "ConnectionStrings": {
  "DefaultConnection": "server=ServerName;Database=DatabaseName;Trusted_Connection=True;TrustServerCertificate=True"
},
-Views yapılanması ve Shared: ASP.NET MVC'de Views/Shared klasörü, tüm controller ve action'lar tarafından ortaklaşa kullanılabilen görünümleri (örneğin, _Layout.cshtml veya _Error.cshtml) saklamak için kullanılır.
-Model katmanı: Create Category class.
-Create AppDbContext : EF Core ile çalışma mantığı.
-Added SeedData
-Add-Migration
-Update-Database
-Controllers katmanı:Create CategoryController class and view actions.
-Burada code first yaklaşımı uygulandı.
-Category CRUD işlemleri ve View tasarımları.
-TempData ile çalışma.
-Validasyon işlemleri.

2-)İkinci aşamadaki konular;
-Generic Repository => IRepository
![image](https://github.com/user-attachments/assets/a95b4dc4-16cc-4a88-be1e-06280011d120)

-Generic Repository'i uygulama:
![image](https://github.com/user-attachments/assets/bce353a8-d77f-4f24-a59b-9916921ecfc1)


