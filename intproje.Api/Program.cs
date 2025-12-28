using Microsoft.EntityFrameworkCore;
using intproje.Api.Data;

var builder = WebApplication.CreateBuilder(args);

[cite_start]// 1. Veritabanı (SQLite) Ayarı [cite: 27]
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

[cite_start]// 2. Controller ve JSON Ayarları [cite: 30]
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// 3. Swagger Servis Kaydı (Hata veren yer burasıydı)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Çakışmayı önlemek için 'Microsoft.OpenApi.Models' kısmını açıkça yazdık
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "İş Portalı API", 
        Version = "v1" 
    });
});

[cite_start]// 4. CORS Politikası [cite: 25]
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