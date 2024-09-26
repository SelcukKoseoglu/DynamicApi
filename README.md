# Dynamic API Project

## Proje Hakkında
Dynamic API, dinamik nesne yönetimi ve işlem gerçekleştirme özelliklerine sahip bir .NET Core Web API projesidir. Bu proje, farklı türde nesneleri (ürünler, siparişler vb.) oluşturma, güncelleme ve listeleme gibi CRUD işlemlerini destekler.

## Özellikler
- Dinamik nesne oluşturma ve yönetimi
- Sipariş ve ürün işlemleri
- JSON formatında veri girişi ve çıkışı
- Hata yönetimi ve validasyon
- Transaction yönetimi

## Teknolojiler
- .NET 8
- Entity Framework Core
- FluentValidation
- JSON.NET
- OpenAPI (Swagger)

## Kurulum
### Gereksinimler
- .NET SDK 8.0 veya üzeri
- Visual Studio 2022 veya Visual Studio Code
- SQL Server veya başka bir ilişkisel veritabanı

### Proje Kurulumu
1. Projeyi klonlayın: `git clone <repository-url>` ardından `cd <project-directory>` komutunu çalıştırın.
2. Gerekli paketleri yükleyin: `dotnet restore` komutunu çalıştırın.
3. Veritabanı migrasyonlarını oluşturun ve uygulayın: `dotnet ef migrations add InitialCreate` ve `dotnet ef database update` komutlarını sırasıyla çalıştırın.
4. Uygulamayı başlatın: `dotnet run` komutunu kullanın.
5. API'yi tarayıcıda veya Postman gibi bir API istemcisinde test edin.

## Docker Desteği

Docker Compose ile projeyi çalıştırmak için aşağıdaki adımları izleyin:

1. Proje kök dizinine gidin.
2. Şu komutu çalıştırın:
docker-compose up -d --build
API, http://localhost:4552 adresinden erişilebilir olacaktır.


## API Kullanımı
### Dinamik Nesne Oluşturma
**Endpoint:** `POST /api/Object/create`  
**Request Body:**
```json
{
  "objectType": "product",
  "data": {
    "name": "Laptop",
    "price": 1000.00,
    "quantity": 2
  }
}


### Transaction oluşturma
**Endpoint:** `POST /api/Object/createTransaction`  
**Request Body:**
```json
{
  "orderData": {
    "customerId": 123,
    "orderDate": "2024-09-26T12:00:00Z",
    "totalAmount": 1000.00
  },
  "products": [
    {
      "name": "Laptop",
      "price": 1000.00,
      "quantity": 1
    },
    {
      "name": "Mouse",
      "price": 25.00,
      "quantity": 2
    }
  ]
}

### Veri çekme
**Endpoint:** `GET /api/Object/{type}/{id}`

### Veri güncelleme
**Endpoint:** `PUT /api/Object/{id}`

### Veri silme
**Endpoint:** `DELETE /api/Object/{id}`

