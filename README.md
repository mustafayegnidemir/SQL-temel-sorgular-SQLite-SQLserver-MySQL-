# SQL-temel-sorgular-SQLite-SQLserver-MySQL-

Scalfolding --> Burada haricen oluşturulmuş veri tabanının .net ortamına aktarılması işlemi yapılıyor.
Usage: dotnet ef dbcontext scaffold [arguments] [options]
Arguments:
  <CONNECTION>  The connection string to the database.
  <PROVIDER>    The provider to use. (E.g. Microsoft.EntityFrameworkCore.SqlServer)
Options: 

Terminale Mysql için connection ve  providerı oluştrulmuş aşağıdaki satırlar yazılır.
  
dotnet ef dbcontext scaffold 
"server=localhost;port=3306;database=Northwind;user=root;password=mysql123;" "Pomelo.EntityFrameworkCore.MySql" 
--output-dir "Data/EfCore" --context NorthwindContext


