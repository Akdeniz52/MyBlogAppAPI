# MyBlogApp API

## 📋 Proje Hakkında

MyBlogApp API, blog uygulaması için geliştirilmiş RESTful bir Web API servisidir. Kullanıcıların blog yazıları oluşturmasına, düzenlemesine, silmesine ve yorum yapmasına olanak tanır. Modern güvenlik standartları ve best practice'ler kullanılarak geliştirilmiştir.

## 🚀 Özellikler

- **Kullanıcı Yönetimi**: Kayıt, giriş, profil görüntüleme ve düzenleme
- **Blog Yazıları**: Oluşturma, düzenleme, silme ve listeleme
- **Yorum Sistemi**: Yorum ekleme, düzenleme ve silme
- **Rol Tabanlı Yetkilendirme**: Admin ve kullanıcı rolleri
- **Dosya Yükleme**: Profil ve blog yazısı görselleri için güvenli dosya yükleme
- **JWT Authentication**: Token tabanlı kimlik doğrulama
- **Swagger UI**: API dokümantasyonu ve test arayüzü

## 🛠️ Kullanılan Teknolojiler

### Backend Framework
- **.NET 8.0**: Modern, yüksek performanslı framework
- **ASP.NET Core Web API**: RESTful API geliştirme framework'ü

### Veritabanı
- **Entity Framework Core 8.0.5**: ORM (Object-Relational Mapping)
- **SQLite**: Hafif ve taşınabilir veritabanı

### Kimlik Doğrulama ve Yetkilendirme
- **ASP.NET Core Identity**: Kullanıcı yönetimi ve kimlik doğrulama
- **JWT Bearer Authentication**: Token tabanlı kimlik doğrulama
- **System.IdentityModel.Tokens.Jwt 8.10.0**: JWT token işlemleri

### API Dokümantasyonu
- **Swashbuckle.AspNetCore 6.6.2**: Swagger/OpenAPI dokümantasyonu

### Mimari
- **Repository Pattern**: Veri erişim katmanı soyutlaması
- **DTO (Data Transfer Object)**: Veri transfer nesneleri
- **Dependency Injection**: Bağımlılık enjeksiyonu

## 🔒 Güvenlik Önlemleri

### 1. Kimlik Doğrulama (Authentication)
- **JWT Bearer Token**: Güvenli token tabanlı kimlik doğrulama
- **Password Hashing**: ASP.NET Core Identity ile otomatik şifre hashleme
- **Token Validation**: Token geçerlilik kontrolü (süre, imza, issuer)
- **Token Expiration**: Token'ların 1 gün sonra otomatik süresi dolması

### 2. Yetkilendirme (Authorization)
- **Role-Based Access Control (RBAC)**: Rol tabanlı erişim kontrolü
- **[Authorize] Attribute**: Korumalı endpoint'ler için yetkilendirme
- **[Authorize(Roles = "admin")]**: Admin rolüne özel endpoint'ler
- **User Ownership Validation**: Kullanıcıların sadece kendi içeriklerini düzenleyebilmesi

### 3. Veri Güvenliği
- **Input Validation**: ModelState validation ile giriş doğrulama
- **SQL Injection Prevention**: Entity Framework Core ile parametreli sorgular
- **XSS Protection**: DTO kullanımı ile veri sanitizasyonu
- **HTTPS Redirection**: Güvenli bağlantı yönlendirmesi

### 4. Dosya Yükleme Güvenliği
- **Guid-Based File Naming**: Dosya adlarında tahmin edilemezlik
- **File Extension Validation**: Dosya uzantısı kontrolü
- **Secure File Storage**: wwwroot klasöründe güvenli saklama
- **Path Traversal Prevention**: Güvenli dosya yolu oluşturma

### 5. CORS (Cross-Origin Resource Sharing)
- **Restricted Origins**: Sadece belirli origin'lerden isteklere izin
- **Policy-Based CORS**: "AllowClient" policy ile kontrollü erişim

### 6. API Güvenliği
- **Swagger Security Definition**: Swagger'da JWT token desteği
- **Bearer Token Format**: Standart Bearer token formatı
- **JSON Serialization Security**: Reference cycle prevention

### 7. Şifre Politikaları
- **Minimum Length**: En az 6 karakter şifre zorunluluğu
- **Identity Password Options**: Esnek şifre politikaları (geliştirme ortamı için)

## 📁 Proje Yapısı

```
MyBlogAppAPI/
├── Controllers/          # API Controller'ları
│   ├── UserController.cs
│   ├── PostsController.cs
│   └── CommentController.cs
├── Data/                # Veri erişim katmanı
│   ├── Abstract/        # Repository interface'leri
│   └── Concrete/        # Repository implementasyonları
│       └── EfCore/      # EF Core repository'ler
├── DTO/                 # Data Transfer Objects
├── Entity/              # Veritabanı entity'leri
├── Migrations/          # EF Core migrations
├── wwwroot/             # Statik dosyalar
│   ├── img/            # Blog yazısı görselleri
│   └── uploads/        # Kullanıcı yüklemeleri
├── Program.cs           # Uygulama başlangıç dosyası
└── appsettings.json     # Yapılandırma dosyası
```

## 🔧 Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Visual Studio 2022 veya Visual Studio Code
- SQLite (Entity Framework Core ile birlikte gelir)

### Adımlar

1. **Projeyi klonlayın veya indirin**
   ```bash
   cd MyBlogAppAPI
   ```

2. **Bağımlılıkları yükleyin**
   ```bash
   dotnet restore
   ```

3. **Veritabanı migration'larını çalıştırın**
   ```bash
   dotnet ef database update
   ```

4. **appsettings.json dosyasını yapılandırın**
   ```json
   {
     "ConnectionStrings": {
       "SQLite_Connection": "Data Source=blog.db"
     },
     "AppSettings": {
       "Secret": "YourSuperSecretKeyForJWTTokenGeneration",
       "BaseImageUrl": "http://localhost:5261/"
     }
   }
   ```

5. **Uygulamayı çalıştırın**
   ```bash
   dotnet run
   ```

6. **Swagger UI'ya erişin**
   - Tarayıcınızda `https://localhost:5261/swagger` adresine gidin

## 📡 API Endpoints

### Kullanıcı İşlemleri
- `POST /api/user/register` - Kullanıcı kaydı
- `POST /api/user/login` - Kullanıcı girişi
- `GET /api/user/profile/{username}` - Kullanıcı profili (Auth gerekli)
- `PUT /api/user/edit` - Profil düzenleme (Auth gerekli)
- `GET /api/user/roles/{username}` - Kullanıcı rolleri (Auth gerekli)

### Blog Yazıları
- `GET /api/posts/list` - Aktif blog yazılarını listele
- `GET /api/posts/details/{id}` - Blog yazısı detayları
- `POST /api/posts/create-post` - Yeni blog yazısı oluştur (Auth gerekli)
- `GET /api/posts/my-post-list` - Kullanıcının blog yazıları (Auth gerekli)
- `PUT /api/posts/edit-post` - Blog yazısı düzenle (Auth gerekli)
- `DELETE /api/posts/delete/{id}` - Blog yazısı sil (Auth gerekli)
- `GET /api/posts/adminpanelpost` - Tüm blog yazıları (Admin gerekli)

### Yorumlar
- `POST /api/comment/add` - Yorum ekle (Auth gerekli)
- `PUT /api/comment/update/{id}` - Yorum düzenle (Auth gerekli, sadece kendi yorumu)
- `DELETE /api/comment/delete/{id}` - Yorum sil (Auth gerekli, sadece kendi yorumu)

## 🔑 JWT Token Kullanımı

API'ye istek gönderirken, korumalı endpoint'ler için Authorization header'ında JWT token göndermeniz gerekir:

```
Authorization: Bearer {your_jwt_token}
```

Token, login endpoint'inden alınır ve 24 saat geçerlidir.

## 🧪 Test

Swagger UI üzerinden API endpoint'lerini test edebilirsiniz:
1. Swagger UI'da login endpoint'ini kullanarak token alın
2. "Authorize" butonuna tıklayın
3. Token'ı "Bearer {token}" formatında girin
4. Diğer endpoint'leri test edin

## 📝 Notlar

- Geliştirme ortamında HTTPS zorunluluğu kapatılmıştır (`RequireHttpsMetadata = false`)
- Production ortamında mutlaka HTTPS kullanılmalıdır
- JWT Secret key'i güvenli bir şekilde saklanmalıdır (Environment Variables veya Azure Key Vault)
- CORS policy production'da daha kısıtlayıcı olmalıdır

## 👤 Geliştirici

**Abdullah Akdeniz**  


