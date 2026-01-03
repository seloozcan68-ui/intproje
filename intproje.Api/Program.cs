using Microsoft.EntityFrameworkCore;
using intproje.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı (SQLite) Ayarı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Controller ve JSON Ayarları
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// 3. Swagger Servis Kaydı
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. CORS Politikası
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMvc", policy =>
    {
        policy.WithOrigins("http://localhost:5101", "https://localhost:7248")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// 5. Geliştirme Ortamı Ayarları
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "İş Portalı API v1");
        c.RoutePrefix = string.Empty; // Uygulama açılınca direkt Swagger gelsin
    });
}

app.UseCors("AllowMvc");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();