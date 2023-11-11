using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.API.Data;
using WEB_153505_PIKHTOVNIKAVA.API.Services.ProductService;
using WEB_153505_PIKHTOVNIKAVA.API.Services;
using WEB_153505_PIKHTOVNIKAVA.API.Services.SeasonCategoryService;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton(typeof(ConfigurationService));

// íóæåí äëÿ ïîëó÷åíèÿ ìàêèñìàëüíîãî êîëè÷åñòâà ñòðàíèö â
// productService
builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(ISeasonCategoryService), typeof(SeasonCategoryService));

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.Authority = builder
    .Configuration
    .GetSection("isUri").Value;
    opt.TokenValidationParameters.ValidateAudience = false;
    opt.TokenValidationParameters.ValidTypes =
    new[] { "at+jwt" };
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddMvc()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.WriteIndented = true; options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


var app = builder.Build();

await DbInitialazer.SeedData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
