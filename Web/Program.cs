using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // 未登入會導向這裡
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cookie 有效期
        options.SlidingExpiration = true;           // 每次存取都會延長有效期
    })
    .AddJwtBearer(options => //指定使用 JWT Bearer 驗證方案改用 api 後可使用這個解法 [Authorize(AuthenticationSchemes = "JwtBearer")] 
    {
        // 設定 JWT Token 驗證參數
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  // 是否驗證發行者 (Issuer)，避免不明來源 Token
            ValidateAudience = true,  // 是否驗證接收者 (Audience)，確保 Token 是給正確的應用程式
            ValidateLifetime = true,  // 是否驗證 Token 是否過期，提升安全性
            ValidateIssuerSigningKey = true,  // 是否驗證簽名金鑰，確保 Token 未被篡改

            ValidIssuer = "ecom",  // 設定合法的發行者 (Issuer)
            ValidAudience = "web",  // 設定合法的接收者 (Audience)

            // 設定加密金鑰 (這個金鑰必須與 JWT 產生時使用的相同) todo : 更換成 config 寫法
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey123456789101112131415!")),

            // 讓 [Authorize(Roles = "...")] 能辨識角色
            RoleClaimType = ClaimTypes.Role
        };
        // Debug: 打印每次 token 驗證時的詳細訊息
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

// 註冊授權服務
builder.Services.AddAuthorization();

// Configure SQL Server 
Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// add Services Scope
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddSession();

// 註冊 IHttpClientFactory
builder.Services.AddHttpClient();

var app = builder.Build();

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

app.UseAuthentication(); // 啟用身份驗證 (驗證 Token)
app.UseAuthorization();  // 啟用授權 (確保用戶有存取權限)
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
