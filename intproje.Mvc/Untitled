🛡️ İş İlanı Başvuru Platformu - Proje Aksiyon Planı
Bu belge, ASP.NET Core MVC + Web API mimarisiyle geliştirilecek olan, SQLite ve EF Core tabanlı "İş İlanı Başvuru Platformu" projesinin tüm geliştirme aşamalarını içerir.


🚀 Faz 1: Ortam Hazırlığı ve Proje Kurulumu
VS Code üzerinde bir Solution yapısının kurgulanması.


JobPortal.API projesinin (Web API) oluşturulması.


JobPortal.Web projesinin (MVC) oluşturulması.


Gerekli NuGet paketlerinin yüklenmesi:


Microsoft.EntityFrameworkCore.Sqlite.


Microsoft.EntityFrameworkCore.Design.

Microsoft.EntityFrameworkCore.Tools.

🏗️ Faz 2: Veri Modeli ve Veritabanı (Code-First)

Models klasöründe en az 3 ilişkili tablonun oluşturulması:




Company (Şirketler).



JobPosting (İlanlar).



Applicant (Adaylar ve CV Özetleri).



AppDbContext sınıfının tanımlanması ve SQLite bağlantı dizesinin yapılandırılması.


VS Code Terminal üzerinden Migration işlemlerinin yapılması:


dotnet ef migrations add InitialCreate

dotnet ef database update

⚙️ Faz 3: Web API Geliştirme (Backend)
API Controller'larının JSON formatında veri dönecek şekilde yazılması.



CompaniesController, JobsController ve ApplicantsController içinde HTTP metotlarının (GET, POST, PUT, DELETE) yapılandırılması.

Tablolar arası ilişkilerin (One-to-Many) API tarafında doğru yönetilmesi.


💻 Faz 4: MVC Client Geliştirme (UI)
API'ye istek atacak bir ApiService katmanının HttpClient ile oluşturulması.


Bootstrap kullanılarak responsive bir arayüz tasarlanması.


View yapılarının oluşturulması:

İlan Listeleme, İlan Detay ve Başvuru Formu.


Şirket ve Aday yönetim ekranları.

CRUD işlemlerinin (Ekleme, Silme, Güncelleme, Listeleme) arayüze bağlanması.

🧪 Faz 5: Test ve Teslimat Hazırlığı
Projenin derleme hatalarından arındırılması ve çalışabilirliğinin kontrolü.

SQLite veritabanı üzerindeki ilişkilerin doğruluğunun teyidi.

Proje kaynak kodlarının ve kurulum talimatlarını içeren dökümanın hazırlanması.

Dosyaların 24219043 klasör adı ile sıkıştırılarak teslimata hazır hale getirilmesi