# Ecommerce
## 簡介
此專案使用 dotnet8 MVC 建置，基於 microsoft eShopOnWeb 專案做適當的簡化，使其環境能夠更好上手，更適用真實快速開發的環境

## 架構說明
此程式使用 identity 作為範例，實做 DDD 架構

## Domain (ApplicationCore)
討論好的功能會整理成 Domain ，並依照 Domain 建立資料表
#### Entities (Aggregate Root 、 Entity、Value Object、 Domain Event)
藉由繼承 IAggregateRoot 辨別 Aggregate Root
#### Service 
ApplicationCore 邏輯在此處實做，並注入 IRepository 完成資料庫的操作，並作為對外的接口
此處建立資料庫統一入口
### Specifications
在此建立 query 語法 read 的統一入口

## Infrastructure
建立好 Domain 後需要進到 Infrastructure 設定 EF 資料庫 (Code First)
#### EfRepository.cs
此處建立資料庫統一入口，與必須要繼承 IAggregateRoot 才能調用

## Web
dotnet MVC 架構
#### Service
在此實做 ApplicationCore 的 Service，將產出的邏輯套用到網頁中

## UniTest
對 Service 的 function 項目進行測試


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



This project is adapted from the Microsoft eShopOnWeb project and is for non-profit use
This project is not responsible for any damage or loss caused by citing
