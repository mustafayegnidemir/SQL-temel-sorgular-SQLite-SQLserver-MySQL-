# SQLite-temel-sorgular
.csproj da iplgili değişiklikler yapılır.
ilgili sql database ine ait packeage lar
 -SQL Server için 'dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.9'
 -MySQL için 'dotnet add package Pomelo.EntityFrameworkCore.MySql --version 6.0.2'
 -SQLite için 'dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.9'
 
  'dotnet tool install  --global dotnet-ef'
  'dotnet add package Microsoft.EntityFrameworkCore.Design'

Console da SQL sorgularını dönüştürülmüş halini görmek için
  'dotnet add package Microsoft.Extensions.Logging.Console --version 6.0.0'
  
önce istenilen tablolara dair class lar oluşturulur.
Uygulamaya ait context class ı oluşturulur.

Migration klasörünün oluşturulması gerekiyor.
'dotnet ef migrations add InitialCreate'
'dotnet ef database update'

Migration klasör oluşturduktan sonra gelecek updateler için örneğin coloumn ekleme 
-tablolara coloumn eklendikten sonra migration da update gereklidir. database tarafında değişiklikleri görmek içinde extra update gerekiyor.
Orneğin  
  'dotnet ef migrations add addColoumnProductCaategoryId' 
  'dotnet ef database update'
  
Migration güncellemeleri esnasında eski migration lara dönme işlemi için dönmek istenilen migration-name ile yapılıyor.
 'dotnet ef database update addColoumnProductCategoryId'
 
Migarations klasöründen Son Migration işlemini silmek için
 'dotnet ef migrations remove'

 
Tüm program.cs de kullanılacak metotlar eklenir.


        AddProduct();
        GetAllProducts();
        GetAllProductById(1);
        GetAllProductByName("samsung");
        UpdateProduct();
        DeleteProduct(13);
