# Ecommerce
## 簡介
此專案使用 dotnet8 MVC 建置，基於 microsoft eShopOnWeb 專案做進階的優化，使其環境能夠更好的被使用者使用，與更貼近 Production 的環境
## api 使用說明
    1. migration 
    -- create migration (from Web folder CLI)
        dotnet ef migrations add InitialModel -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations
        dotnet ef database update -c IdentityContext -p ../Infrastructure.csproj -s Web.csproj
    2. 该命令用于生成并信任本地开发环境的 HTTPS 证书，确保你的 .NET Core / ASP.NET Core 应用在本地可以使用 HTTPS 运行，而不会出现证书警告。
    dotnet dev-certs https --trust		
    3. SQL 連線
    找到 appsetting.json (此範例為本機資料庫連線字串)	  
    "ConnectionStrings": {
        "CatalogConnection": "Data Source=localhost;Initial Catalog=Ecommerce.Identity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
    }


##### This project is adapted from the Microsoft eShopOnWeb project and is for non-profit use
##### This project is not responsible for any damage or loss caused by citing