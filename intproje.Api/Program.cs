using Microsoft.EntityFrameworkCore;
using intproje.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext yapılandırması
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller servisleri - JSON döngüsel referans sorununu çözmek için
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddOpenApi();

// CORS yapılandırması - MVC'nin API'ye erişebilmesi için
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMvc", policy =>
    {
        policy.WithOrigins(
                "https://localhost:7248", 
                "http://localhost:5031",
                "http://localhost:5101"
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// CORS'u routing'den önce kullan
app.UseCors("AllowMvc");

// Development'ta HTTPS redirect'i kapatıyoruz
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

