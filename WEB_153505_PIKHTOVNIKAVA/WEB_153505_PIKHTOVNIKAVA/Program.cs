using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using WEB_153505_PIKHTOVNIKAVA.Models;
using WEB_153505_PIKHTOVNIKAVA.Services.ProductService;
using WEB_153505_PIKHTOVNIKAVA.Services.SeasonCategoryService;
//using WEB_153505_PIKHTOVNIKAVA.Services.ProductService;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

 

var ApiUri = builder.Configuration["UriData:ApiUri"];

builder.Services.AddRazorPages();

builder.Services.AddScoped(typeof(Cart), sp => SessionCart.GetCart(sp));

builder.Services
.AddHttpClient<IProductService, ApiProductService>(opt =>
opt.BaseAddress = new Uri(ApiUri));

builder.Services
.AddHttpClient<ISeasonCategoryService, ApiSeasonCategoryService>(opt =>
opt.BaseAddress = new Uri(ApiUri));


builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "cookie";
    opt.DefaultChallengeScheme = "oidc";
})
.AddCookie("cookie")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
    options.ClientId = builder.Configuration["InteractiveServiceSettings:ClientId"];
    options.ClientSecret = builder.Configuration["InteractiveServiceSettings:ClientSecret"];
    // Ïîëó÷èòü Claims ïîëüçîâàòåëÿ
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ResponseType = "code";
    options.ResponseMode = "query";
    options.SaveTokens = true;
});


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

app.UseAuthentication();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.MapRazorPages().RequireAuthorization();

app.Run();
