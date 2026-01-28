# MyBlogApp Client

## 📋 Proje Hakkında

MyBlogApp Client, blog uygulaması için geliştirilmiş bir ASP.NET Core MVC web uygulamasıdır. MyBlogApp API ile iletişim kurarak kullanıcılara blog yazılarını görüntüleme, oluşturma, düzenleme ve yorum yapma imkanı sunar. Modern ve kullanıcı dostu bir arayüze sahiptir.

## 🚀 Özellikler

- **Blog Yazıları**: Blog yazılarını listeleme ve detaylarını görüntüleme
- **Kullanıcı İşlemleri**: Kayıt, giriş ve profil yönetimi
- **İçerik Yönetimi**: Blog yazısı oluşturma, düzenleme ve silme
- **Yorum Sistemi**: Blog yazılarına yorum ekleme
- **Admin Paneli**: Admin kullanıcıları için özel yönetim paneli
- **Responsive Tasarım**: Mobil uyumlu arayüz
- **JWT Token Yönetimi**: Güvenli token tabanlı kimlik doğrulama

## 🛠️ Kullanılan Teknolojiler

### Backend Framework
- **.NET 8.0**: Modern, yüksek performanslı framework
- **ASP.NET Core MVC**: Model-View-Controller mimarisi

### Frontend
- **Razor Views**: Server-side rendering
- **Bootstrap**: Responsive CSS framework
- **JavaScript**: Client-side işlemler ve API çağrıları
- **jQuery**: DOM manipülasyonu (Bootstrap bağımlılığı)

### API İletişimi
- **HttpClient**: RESTful API ile iletişim
- **System.Text.Json**: JSON serialization/deserialization
- **Fetch API**: Modern JavaScript API çağrıları

### Veri Yönetimi
- **localStorage**: JWT token ve kullanıcı bilgilerinin tarayıcıda saklanması
- **DTO (Data Transfer Object)**: API'den gelen verilerin modellenmesi

## 🔒 Güvenlik Önlemleri

### 1. Client-Side Authentication
- **JWT Token Storage**: Token'lar localStorage'da güvenli şekilde saklanır
- **Token Validation**: Her API isteğinde token kontrolü
- **Automatic Token Injection**: API isteklerinde otomatik token ekleme
- **Token Expiration Handling**: Token süresi dolduğunda otomatik logout

### 2. Authorization Checks
- **Role-Based UI**: Kullanıcı rolüne göre UI elementlerinin gösterilmesi
- **Client-Side Role Validation**: JWT token'dan rol bilgisinin çıkarılması
- **Protected Routes**: Yetkisiz kullanıcıların korumalı sayfalara erişiminin engellenmesi

### 3. Input Validation
- **Model Validation**: ASP.NET Core model validation
- **Client-Side Validation**: JavaScript ile anlık doğrulama
- **Server-Side Validation**: API'ye gönderilmeden önce kontrol

### 4. XSS (Cross-Site Scripting) Koruması
- **Razor HTML Encoding**: Razor view engine'in otomatik HTML encoding'i
- **Content Security**: Kullanıcı girdilerinin sanitize edilmesi

### 5. CSRF (Cross-Site Request Forgery) Koruması
- **ASP.NET Core Anti-Forgery**: Otomatik CSRF token yönetimi
- **Same-Origin Policy**: API ile aynı origin kontrolü

### 6. Secure API Communication
- **HTTPS**: Güvenli bağlantı kullanımı
- **Bearer Token Authentication**: Standart token formatı
- **Error Handling**: Hata durumlarında güvenli mesajlaşma

### 7. Session Management
- **localStorage Security**: Token'ların güvenli saklanması
- **Logout Functionality**: Token'ların temizlenmesi
- **Automatic Session Check**: Sayfa yüklendiğinde otomatik oturum kontrolü

## 📁 Proje Yapısı

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

## 🔧 Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Visual Studio 2022 veya Visual Studio Code
- MyBlogApp API'nin çalışıyor olması (http://localhost:5261)

### Adımlar

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

## 🔑 JWT Token Yönetimi

### Token Alma
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

### Token Kullanımı
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

### Token Kontrolü
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

## 📱 Responsive Tasarım

Uygulama Bootstrap framework'ü kullanılarak geliştirilmiştir ve tüm ekran boyutlarında (mobil, tablet, desktop) düzgün çalışır.

## 🔄 API Entegrasyonu

Client uygulaması, MyBlogApp API ile şu şekilde iletişim kurar:

1. **Public Endpoints**: Token gerektirmeyen endpoint'ler (blog listesi, detay)
2. **Protected Endpoints**: Token gerektiren endpoint'ler (oluşturma, düzenleme, silme)
3. **Role-Based Endpoints**: Admin rolü gerektiren endpoint'ler

## 🐛 Hata Yönetimi

- API hatalarında kullanıcıya anlaşılır mesajlar gösterilir
- 401 (Unauthorized) hatalarında otomatik logout
- 404 (Not Found) hatalarında uygun sayfa yönlendirmesi
- Network hatalarında kullanıcı bilgilendirmesi

## 📝 Notlar

- API URL'i `appsettings.json` veya environment variable'larda yapılandırılabilir
- Production ortamında API URL'i mutlaka HTTPS olmalıdır
- localStorage kullanımı XSS saldırılarına karşı dikkatli olunmalıdır
- Token'lar production'da daha güvenli bir yerde (httpOnly cookie) saklanabilir

## 🚀 Geliştirme Önerileri

- **State Management**: Daha karmaşık uygulamalar için Redux veya benzeri bir state management kütüphanesi
- **Error Handling**: Global error handler middleware
- **Loading States**: API çağrıları sırasında loading göstergeleri
- **Form Validation**: Daha gelişmiş client-side validation
- **Caching**: API yanıtlarının cache'lenmesi

## 👤 Geliştirici

**Abdullah Akdeniz**

