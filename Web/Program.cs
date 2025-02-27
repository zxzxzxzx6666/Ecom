using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// 註冊身份驗證服務，指定使用 JWT Bearer 驗證方案
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // 設定 JWT Token 驗證參數
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  // 是否驗證發行者 (Issuer)，避免不明來源 Token
            ValidateAudience = true,  // 是否驗證接收者 (Audience)，確保 Token 是給正確的應用程式
            ValidateLifetime = true,  // 是否驗證 Token 是否過期，提升安全性
            ValidateIssuerSigningKey = true,  // 是否驗證簽名金鑰，確保 Token 未被篡改

            ValidIssuer = "YourApp",  // 設定合法的發行者 (Issuer)
            ValidAudience = "YourApp",  // 設定合法的接收者 (Audience)

            // 設定加密金鑰 (這個金鑰必須與 JWT 產生時使用的相同)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey123456!"))
        };
    });

// Configure SQL Server 
Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// add Services Scope
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

// 設定中間件 (Middleware) 順序
app.UseAuthentication(); // 啟用身份驗證 (驗證 Token)
app.UseAuthorization();  // 啟用授權 (確保用戶有存取權限)

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
