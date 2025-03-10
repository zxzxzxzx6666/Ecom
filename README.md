﻿# Ecommerce
## 簡介
此專案使用 dotnet8 MVC 建置，基於 microsoft eShopOnWeb 專案做適當的簡化，使其環境能夠更好上手，更適用真實快速開發的環境

## 架構說明
此程式使用 identity 作為範例，實做 DDD 架構
### Domain
討論好的功能會整理成 Domain ，並依照 Domain 建立資料表

#### Aggregate Root

### Infrastructure


## api 使用說明
    1. migration 
    -- create migration (from Web folder CLI)
        dotnet ef migrations add InitialModel -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations
        dotnet ef database update -c IdentityContext -p ../Infrastructure.csproj -s Web.csproj
    2. 该命令用于生成并信任本地开发环境的 HTTPS 证书，确保你的 .NET Core / ASP.NET Core 应用在本地可以使用 HTTPS 运行，而不会出现证书警告。
    dotnet dev-certs https --trust		
    3. SQL connection  
    find appsetting.json
    "ConnectionStrings": {
        "CatalogConnection": "Data Source=localhost;Initial Catalog=Ecommerce.Identity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
    }



##### This project is adapted from the Microsoft eShopOnWeb project and is for non-profit use
##### This project is not responsible for any damage or loss caused by citing
