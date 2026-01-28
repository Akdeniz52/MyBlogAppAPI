# MyBlogApp - Blog Uygulaması Projesi

## 📋 Proje Hakkında

MyBlogApp, blog yazıları oluşturma, düzenleme, görüntüleme ve yorum yapma imkanı sunan modern bir web uygulamasıdır. Proje iki ana bileşenden oluşmaktadır:

1. **MyBlogApp API**: RESTful Web API servisi (Backend)
2. **MyBlogApp Client**: ASP.NET Core MVC web uygulaması (Frontend)

Modern güvenlik standartları, JWT authentication ve rol tabanlı yetkilendirme kullanılarak geliştirilmiştir.

---

## 🚀 Proje Özellikleri

### Genel Özellikler
- **Kullanıcı Yönetimi**: Kayıt, giriş, profil görüntüleme ve düzenleme
- **Blog Yazıları**: Oluşturma, düzenleme, silme ve listeleme
- **Yorum Sistemi**: Blog yazılarına yorum ekleme, düzenleme ve silme
- **Rol Tabanlı Yetkilendirme**: Admin ve kullanıcı rolleri
- **Dosya Yükleme**: Profil ve blog yazısı görselleri için güvenli dosya yükleme
- **JWT Authentication**: Token tabanlı kimlik doğrulama
- **Responsive Tasarım**: Mobil uyumlu arayüz
- **Admin Paneli**: Admin kullanıcıları için özel yönetim paneli

---

## 🛠️ Kullanılan Teknolojiler

### Backend (API)
- **.NET 8.0**: Modern, yüksek performanslı framework
- **ASP.NET Core Web API**: RESTful API geliştirme framework'ü
- **Entity Framework Core 8.0.5**: ORM (Object-Relational Mapping)
- **SQLite**: Hafif ve taşınabilir veritabanı
- **ASP.NET Core Identity**: Kullanıcı yönetimi ve kimlik doğrulama
- **JWT Bearer Authentication**: Token tabanlı kimlik doğrulama
- **System.IdentityModel.Tokens.Jwt 8.10.0**: JWT token işlemleri
- **Swashbuckle.AspNetCore 6.6.2**: Swagger/OpenAPI dokümantasyonu
- **Repository Pattern**: Veri erişim katmanı soyutlaması
- **DTO (Data Transfer Object)**: Veri transfer nesneleri
- **Dependency Injection**: Bağımlılık enjeksiyonu

### Frontend (Client)
- **.NET 8.0**: Modern, yüksek performanslı framework
- **ASP.NET Core MVC**: Model-View-Controller mimarisi
- **Razor Views**: Server-side rendering
- **Bootstrap**: Responsive CSS framework
- **JavaScript**: Client-side işlemler ve API çağrıları
- **jQuery**: DOM manipülasyonu (Bootstrap bağımlılığı)
- **HttpClient**: RESTful API ile iletişim
- **System.Text.Json**: JSON serialization/deserialization
- **Fetch API**: Modern JavaScript API çağrıları
- **localStorage**: JWT token ve kullanıcı bilgilerinin tarayıcıda saklanması

---

## 🔒 Güvenlik Önlemleri

### API Güvenliği

#### 1. Kimlik Doğrulama (Authentication)
- **JWT Bearer Token**: Güvenli token tabanlı kimlik doğrulama
- **Password Hashing**: ASP.NET Core Identity ile otomatik şifre hashleme
- **Token Validation**: Token geçerlilik kontrolü (süre, imza, issuer)
- **Token Expiration**: Token'ların 1 gün sonra otomatik süresi dolması

#### 2. Yetkilendirme (Authorization)
- **Role-Based Access Control (RBAC)**: Rol tabanlı erişim kontrolü
- **[Authorize] Attribute**: Korumalı endpoint'ler için yetkilendirme
- **[Authorize(Roles = "admin")]**: Admin rolüne özel endpoint'ler
- **User Ownership Validation**: Kullanıcıların sadece kendi içeriklerini düzenleyebilmesi

#### 3. Veri Güvenliği
- **Input Validation**: ModelState validation ile giriş doğrulama
- **SQL Injection Prevention**: Entity Framework Core ile parametreli sorgular
- **XSS Protection**: DTO kullanımı ile veri sanitizasyonu
- **HTTPS Redirection**: Güvenli bağlantı yönlendirmesi

#### 4. Dosya Yükleme Güvenliği
- **Guid-Based File Naming**: Dosya adlarında tahmin edilemezlik
- **File Extension Validation**: Dosya uzantısı kontrolü
- **Secure File Storage**: wwwroot klasöründe güvenli saklama
- **Path Traversal Prevention**: Güvenli dosya yolu oluşturma

#### 5. CORS (Cross-Origin Resource Sharing)
- **Restricted Origins**: Sadece belirli origin'lerden isteklere izin
- **Policy-Based CORS**: "AllowClient" policy ile kontrollü erişim

#### 6. API Güvenliği
- **Swagger Security Definition**: Swagger'da JWT token desteği
- **Bearer Token Format**: Standart Bearer token formatı
- **JSON Serialization Security**: Reference cycle prevention

#### 7. Şifre Politikaları
- **Minimum Length**: En az 6 karakter şifre zorunluluğu
- **Identity Password Options**: Esnek şifre politikaları (geliştirme ortamı için)

### Client Güvenliği

#### 1. Client-Side Authentication
- **JWT Token Storage**: Token'lar localStorage'da güvenli şekilde saklanır
- **Token Validation**: Her API isteğinde token kontrolü
- **Automatic Token Injection**: API isteklerinde otomatik token ekleme
- **Token Expiration Handling**: Token süresi dolduğunda otomatik logout

#### 2. Authorization Checks
- **Role-Based UI**: Kullanıcı rolüne göre UI elementlerinin gösterilmesi
- **Client-Side Role Validation**: JWT token'dan rol bilgisinin çıkarılması
- **Protected Routes**: Yetkisiz kullanıcıların korumalı sayfalara erişiminin engellenmesi

#### 3. Input Validation
- **Model Validation**: ASP.NET Core model validation
- **Client-Side Validation**: JavaScript ile anlık doğrulama
- **Server-Side Validation**: API'ye gönderilmeden önce kontrol

#### 4. XSS (Cross-Site Scripting) Koruması
- **Razor HTML Encoding**: Razor view engine'in otomatik HTML encoding'i
- **Content Security**: Kullanıcı girdilerinin sanitize edilmesi

#### 5. CSRF (Cross-Site Request Forgery) Koruması
- **ASP.NET Core Anti-Forgery**: Otomatik CSRF token yönetimi
- **Same-Origin Policy**: API ile aynı origin kontrolü

#### 6. Secure API Communication
- **HTTPS**: Güvenli bağlantı kullanımı
- **Bearer Token Authentication**: Standart token formatı
- **Error Handling**: Hata durumlarında güvenli mesajlaşma

#### 7. Session Management
- **localStorage Security**: Token'ların güvenli saklanması
- **Logout Functionality**: Token'ların temizlenmesi
- **Automatic Session Check**: Sayfa yüklendiğinde otomatik oturum kontrolü

---

## 📁 Proje Yapısı

### API Proje Yapısı
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

### Client Proje Yapısı
```
MyBlogAppClient/
├── Controllers/          # MVC Controller'ları
│   ├── HomeController.cs
│   ├── PostController.cs
│   └── UserController.cs
├── Models/              # ViewModel ve DTO'lar
├── Views/               # Razor view dosyaları
│   ├── Home/           # Ana sayfa view'ları
│   ├── Post/           # Blog yazısı view'ları
│   ├── User/           # Kullanıcı view'ları
│   └── Shared/         # Paylaşılan layout ve partial view'lar
├── wwwroot/            # Statik dosyalar
│   ├── css/            # CSS dosyaları
│   ├── js/             # JavaScript dosyaları
│   └── lib/            # Kütüphane dosyaları (Bootstrap, jQuery)
├── Program.cs          # Uygulama başlangıç dosyası
└── appsettings.json    # Yapılandırma dosyası
```

---

## 🔧 Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Visual Studio 2022 veya Visual Studio Code
- SQLite (Entity Framework Core ile birlikte gelir)

### API Kurulumu

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

### Client Kurulumu

1. **Projeyi klonlayın veya indirin**
   ```bash
   cd MyBlogAppClient
   ```

2. **Bağımlılıkları yükleyin**
   ```bash
   dotnet restore
   ```

3. **appsettings.json dosyasını kontrol edin**
   - API URL'inin doğru olduğundan emin olun

4. **Uygulamayı çalıştırın**
   ```bash
   dotnet run
   ```

5. **Tarayıcıda açın**
   - Varsayılan olarak `http://localhost:5121` adresinde çalışır

### Önemli Notlar
- API'nin çalışıyor olması gereklidir (http://localhost:5261)
- Her iki uygulama da aynı anda çalıştırılmalıdır
- API önce başlatılmalıdır

---

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

---

## 🔑 JWT Token Kullanımı

### API'de Token Kullanımı

API'ye istek gönderirken, korumalı endpoint'ler için Authorization header'ında JWT token göndermeniz gerekir:

```
Authorization: Bearer {your_jwt_token}
```

Token, login endpoint'inden alınır ve 24 saat geçerlidir.

### Client'te Token Yönetimi

#### Token Alma
```javascript
// Login işlemi
const response = await fetch("http://localhost:5261/api/user/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password })
});

const data = await response.json();
localStorage.setItem("token", data.token);
```

#### Token Kullanımı
```javascript
// API isteklerinde token kullanımı
const token = localStorage.getItem("token");
fetch("http://localhost:5261/api/posts/create-post", {
    method: "POST",
    headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
    },
    body: JSON.stringify(postData)
});
```

#### Token Kontrolü
```javascript
// Sayfa yüklendiğinde token kontrolü
const token = localStorage.getItem("token");
if (token) {
    // Kullanıcı giriş yapmış
    // Gizli linkleri göster
} else {
    // Kullanıcı giriş yapmamış
    // Giriş linklerini göster
}
```

---

## 🎨 Kullanıcı Arayüzü

### Ana Sayfa
- Tüm aktif blog yazılarının listesi
- Blog yazısı kartları (başlık, açıklama, görsel)
- Sayfalama ve filtreleme

### Blog Yazısı Detay Sayfası
- Tam içerik görüntüleme
- Yazar bilgileri
- Yorum listesi
- Yorum ekleme formu (giriş yapmış kullanıcılar için)

### Kullanıcı İşlemleri
- **Kayıt**: Yeni kullanıcı kaydı (profil fotoğrafı yükleme ile)
- **Giriş**: Email ve şifre ile giriş
- **Profil**: Kullanıcı profili görüntüleme ve düzenleme
- **Blog Yazılarım**: Kullanıcının kendi blog yazılarını görüntüleme

### Admin Paneli
- Tüm blog yazılarını görüntüleme
- Blog yazılarını aktif/pasif yapma
- İçerik yönetimi

---

## 📱 Responsive Tasarım

Uygulama Bootstrap framework'ü kullanılarak geliştirilmiştir ve tüm ekran boyutlarında (mobil, tablet, desktop) düzgün çalışır.

---

## 🔄 API Entegrasyonu

Client uygulaması, MyBlogApp API ile şu şekilde iletişim kurar:

1. **Public Endpoints**: Token gerektirmeyen endpoint'ler (blog listesi, detay)
2. **Protected Endpoints**: Token gerektiren endpoint'ler (oluşturma, düzenleme, silme)
3. **Role-Based Endpoints**: Admin rolü gerektiren endpoint'ler

---

## 🧪 Test

### Swagger UI ile Test
Swagger UI üzerinden API endpoint'lerini test edebilirsiniz:
1. Swagger UI'da login endpoint'ini kullanarak token alın
2. "Authorize" butonuna tıklayın
3. Token'ı "Bearer {token}" formatında girin
4. Diğer endpoint'leri test edin

### Client Test
- Tarayıcıda uygulamayı açın
- Tüm özellikleri manuel olarak test edin
- Farklı kullanıcı rolleri ile test yapın

---

## 🐛 Hata Yönetimi

- API hatalarında kullanıcıya anlaşılır mesajlar gösterilir
- 401 (Unauthorized) hatalarında otomatik logout
- 404 (Not Found) hatalarında uygun sayfa yönlendirmesi
- Network hatalarında kullanıcı bilgilendirmesi

---

## 📝 Önemli Notlar

### API Notları
- Geliştirme ortamında HTTPS zorunluluğu kapatılmıştır (`RequireHttpsMetadata = false`)
- Production ortamında mutlaka HTTPS kullanılmalıdır
- JWT Secret key'i güvenli bir şekilde saklanmalıdır (Environment Variables veya Azure Key Vault)
- CORS policy production'da daha kısıtlayıcı olmalıdır

### Client Notları
- API URL'i `appsettings.json` veya environment variable'larda yapılandırılabilir
- Production ortamında API URL'i mutlaka HTTPS olmalıdır
- localStorage kullanımı XSS saldırılarına karşı dikkatli olunmalıdır
- Token'lar production'da daha güvenli bir yerde (httpOnly cookie) saklanabilir

---

## 🚀 Geliştirme Önerileri

- **State Management**: Daha karmaşık uygulamalar için Redux veya benzeri bir state management kütüphanesi
- **Error Handling**: Global error handler middleware
- **Loading States**: API çağrıları sırasında loading göstergeleri
- **Form Validation**: Daha gelişmiş client-side validation
- **Caching**: API yanıtlarının cache'lenmesi
- **Unit Tests**: Test coverage'ı artırma
- **Integration Tests**: API ve Client entegrasyon testleri

---

## 👤 Geliştirici

**Abdullah Akdeniz**

