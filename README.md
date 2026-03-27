# KayraExportInterview Backend Case Study

## Proje Hakkında

<p>
 Bu case, modern .NET framework üzerinde geliştirilmiş olup, Onion Mimari ve CQRS prensiplerini uygulamakta ve SOLID ile OOP prensiplerini en iyi şekilde pratiğe dökmeyi hedeflemektedir.
</p>
<p>
  Proje; üç ana mikroservisten, 1 worker ve 1 API Gateway' den oluşmaktadır:
  <p>
    <b>Auth Mikro Servis:</b> Kullanıcılar için JWT ve refresh token mekanizması ile güvenli token üretimi sağlanmaktadır. Kimlik doğrulama ve kullanıcı yönetimi, Microsoft Identity Framework kullanılarak uygulanmıştır. Projede veri depolama çözümü olarak SQL Server tercih edilmiştir. Mikro servisteki loglar, Serilog ile oluşturulmakta ve RabbitMQ Sink kütüphanesi kullanılarak asenkron şekilde yayınlanmaktadır.
  </p>
  <p>
    <b>Product Mikro Servis:</b>Ürün ekleme, güncelleme ve listeleme işlemlerini gerçekleştirir. Redis Cache ile performans optimizasyonu yapılmıştır. Projede veri depolama çözümü olarak SQL Server tercih edilmiştir. Mikro servisteki loglar, Serilog ile oluşturulmakta ve RabbitMQ Sink kütüphanesi kullanılarak asenkron şekilde yayınlanmaktadır.
  </p>
  <p>
    <b>Log Mikro Servis Ve Log Listener Worker:</b> Log Listener worker servisi, Auth ve Product mikro servislerinden gelen logları RabbitMQ aracılığıyla asenkron olarak dinler, merkezi bir şekilde toplar ve analiz edilebilir hâle getirir. Logların depolanması için Elasticsearch kullanılmaktadır. Ayrıca, Log Mikro Servis, Elasticsearch’e sorgu gönderen endpointler sağlayarak logların görüntülenmesini mümkün kılar.
  </p>
</p>
<p>
  <b>API Gateway</b>: Frontend uygulamaları için optimize edilmiş ve güvenli enpoint sunmaktadır. Merkezi kimlik doğrulama sağlanmakta ve YARP Gateway ile rate limiting uygulanmaktadır.
</p>

## Kullanılan Teknolojiler
<p>
  <b>Framework:</b> .NET 10
</p>
<p>
  <b>Programlama Dili:</b> C#
</p>
<p>
  <b>Veritabanları:</b> SQL Server, Elastic Search
</p>
<p>
  <b>Cache:</b> Redis
</p>
<p>
  <b>Mesajlaşma Sistemi:</b> RabbitMQ
</p>

## Kullanılan Kütüphaneler
<p>
  <b> Microsoft Identity Framework, JWT:</b> Kullanıcı Üyelik sistemi, kimlik doğrulama ve token üretimi için kullanıldı.
</p>
<p>
  <b>Serilog, Serilog.Sinks.RabbitMQ:</b> Json formatında log üretimi ve RabbitMQ' ya gönderilmesi için kullanıldı.
</p>
<p>
  <b>MassTransit:</b> Event Publisher ile Consumer, Outbox-Inbox patttern ve Retry Strategilerinin uygulanması için kullanıldı.
</p>
<p>
  <b>MediatR</b>: CQRS pattern için command ve query işlemlerinin mediator aracılığıyla ayrıştırılması ve yönetilmesi;
  CommandHandler ve QueryHandler implementasyonlarıyla servisler arası loose coupling sağlanması için kullanıldı.
</p>
<p>
  <b>EF Core:</b> ORM (Object-Relational Mapping) aracı olarak veritabanı ile nesne yönelimli etkileşimin sağlanması, DbContext üzerinden veri erişimi, LINQ ile sorgulama yapılması, Code-First yaklaşımıyla migration yönetimi için kullanıldı.
</p>
<p>
  <b>StackExchange.Redis:</b> Redis ile yüksek performanslı veri erişimi sağlamak ve distributed cache yönetimi için kullanıldı.
</p>
<p>
  <b>Elastic.Clients.Elasticsearch:</b> Elasticsearch ile entegrasyon sağlamak için kullanıldı. Indeksleme ve full-text search işlemleri uygunlandı.
</p>
<p>
  <b>RabbitMQ.Client:</b> RabbitMQ ile doğrudan iletişim kurmak için kullanıldı. queue, exchange ve binding yönetimi, message consume işlemleri uygulandı.
</p>

## Uygulanan Mimari Yaklaşımlar ve Tasarım Prensipleri
<p>
  <b>Repository Pattern:</b> Veri erişim katmanını soyutlamak için kullanıldı; domain katmanı ile veri kaynağı arasındaki bağımlılık azaltıldı, CRUD işlemleri repository’ler üzerinden yönetilerek test edilebilirlik ve sürdürülebilirlik artırıldı.
</p>
<p>
  <b>Unit of Work Pattern:</b> Birden fazla veri erişim işlemini tek bir transaction altında toplamak için kullanıldı; tüm işlemler atomik olarak yönetildi, commit/rollback mekanizmaları ile veri tutarlılığı (consistency) sağlandı.
</p>
<p>
  <b>Outbox Pattern:</b> Veri tabanı işlemleri ile mesaj yayınlama (2 Phase Commit (2PC)) süreçlerinin tutarlı bir şekilde yürütülmesi için kullanıldı; event’ler önce veritabanındaki outbox tablosuna yazıldı, ardından güvenilir şekilde message broker’a iletilerek eventual consistency sağlandı.
</p>
<p>
  <b>Options Pattern:</b> Konfigürasyon ayarlarını strongly-typed sınıflar üzerinden yönetmek için kullanıldı.
</p>
<p>
  <b>CQRS(Command Query Responsibility Segregation):</b> Uygulamada okuma ve yazma sorumluluklarını ayrıştırmak amacıyla uygulandı; CommandHandler’lar veri değişikliklerini yönetirken, QueryHandler’lar sadece okuma işlemlerini gerçekleştirir.
</p>
<p>
  <b>DDD(Domain Driven Design):</b> İş problemlerini yazılım modeline doğru ve sürdürülebilir bir şekilde yansıtmak amacıyla uygulandı; domain odaklı tasarım sayesinde, business logic domain katmanında yoğunlaştırıldı, aggregate’ler ile tutarlılık sağlandı, repository ve service yapıları ile uygulama katmanları arasında loose coupling oluşturuldu.
</p>
  
## Kurulum

##### Repoyu klonla
<code>git clone https://github.com/mfglr/KayraExportInterview.git</code>

##### Proje dizinine gir
<code>cd KayraExportInterview</code>

##### Prod branch'ini indir ve geçiş yap
<p>
  <code>git fetch origin</code>
</p>
<p>
  <code>git checkout -b prod/v1.0.0 origin/prod/v1.0.0</code>
</p>

##### Docker Compose ile build ve çalıştır (arka planda)
<code>docker-compose up --build -d</code>

##### Servis loglarını takip etmek istersen
<code>docker-compose logs -f</code>

## API Dokümantasyonu

### 1) Users

#### 1-1) Create: 
##### HTTP Mehtod: POST
##### URL: /users/create
##### Request Body:

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>email</td>
      <td>string</td>
      <td>Evet</td>
      <td>Kullanıcının e-posta adresi</td>
    </tr>
    <tr>
      <td>password</td>
      <td>string</td>
      <td>Evet</td>
      <td>Kullanıcının şifresi</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği:
<code>
{
 "email": "aliveli@example.com",
 "password": "123456"
}
</code>

 ##### Hatalar

 <table>
  <thead>
    <tr>
      <th>Kod</th>
      <th>Açıklama</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>400</td>
      <td>Email is not valid!</td>
    </tr>
    <tr>
      <td>400</td>
      <td>Email has been already taken!</td>
    </tr>
    <tr>
      <td>400</td>
      <td>Password must be between 6 and 256 characters.</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

##### Response Body
<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Açıklama</th>
      <th>Örnek</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>id</td>
      <td>string (UUID)</td>
      <td>Kullanıcının benzersiz kimliği</td>
      <td>7a508887-758b-4533-a1c7-e479c2e0d832</td>
    </tr>
    <tr>
      <td>token.accessToken</td>
      <td>string (JWT)</td>
      <td>Kullanıcının kimlik doğrulaması için kullanılan geçerli JWT token</td>
      <td>eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9...</td>
    </tr>
    <tr>
      <td>token.refreshToken</td>
      <td>string (UUID)</td>
      <td>Access token süresi dolduğunda yeni token almak için kullanılan refresh token</td>
      <td>1162841d-5832-43f2-b16e-0fdc0ee2c0cb</td>
    </tr>
  </tbody>
</table>

<code>
{
    "id": "7a508887-758b-4533-a1c7-e479c2e0d832",
    "token": {
        "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzNTdkNTBlNS0wZmUxLTRjYzAtODYzNC03NTFkZGU2ZGJjYjEiLCJhdWQiOlsiZ2F0ZXdheS5hcGkiLCJwcm9kdWN0LmFwaSIsInVzZXIuYXBpIl0sImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiN2E1MDg4ODctNzU4Yi00NTMzLWExYzctZTQ3OWMyZTBkODMyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTc3NDYxNjI3NSwiZXhwIjoxNzc0NjE3MTc1LCJpc3MiOiJodHRwOi8vYXV0aHNlcnZpY2UuYXBpIn0.slVrSJRZU1ZmGYsKx7gTUSilm2CFEofeKGjmdktDBnY",
        "refreshToken": "1162841d-5832-43f2-b16e-0fdc0ee2c0cb"
    }
}
</code>

#### 1-2) Login: 
##### HTTP Mehtod: POST
##### URL: /users/login
##### Request Body:

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>key</td>
      <td>string</td>
      <td>Evet</td>
      <td>Kullanıcının e-posta adresi veya kullanıcı adı</td>
    </tr>
    <tr>
      <td>password</td>
      <td>string</td>
      <td>Evet</td>
      <td>Kullanıcının şifresi</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği:
<code>
{
 "key": "aliveli@example.com",
 "password": "123456"
}
</code>

 ##### Hatalar

 <table>
  <thead>
    <tr>
      <th>Kod</th>
      <th>Açıklama</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>404</td>
      <td>User not found!!</td>
    </tr>
    <tr>
      <td>400</td>
      <td>Invalid credentials.</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

##### Response Body
<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Açıklama</th>
      <th>Örnek</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>accessToken</td>
      <td>string (JWT)</td>
      <td>Kullanıcının yetkilendirilmiş istek yapabilmesi için kullanılan geçerli JWT token</td>
      <td>eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9...</td>
    </tr>
    <tr>
      <td>refreshToken</td>
      <td>string (UUID)</td>
      <td>Access token süresi dolduğunda yeni token almak için kullanılan refresh token</td>
      <td>9f65ff34-6cf3-4a05-8a7a-06f0e0414b52</td>
    </tr>
  </tbody>
</table>

<code>
{
    "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4MTcwMzkxYS01YjE4LTQ1YTktOGE5Yi00MmE3N2YyYzUyZjMiLCJhdWQiOlsiZ2F0ZXdheS5hcGkiLCJwcm9kdWN0LmFwaSIsInVzZXIuYXBpIl0sImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiN2E1MDg4ODctNzU4Yi00NTMzLWExYzctZTQ3OWMyZTBkODMyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTc3NDYxNjc3MCwiZXhwIjoxNzc0NjE3NjcwLCJpc3MiOiJodHRwOi8vYXV0aHNlcnZpY2UuYXBpIn0.veZr1xOyqQo6lFviQlsRmJ52dgq8ma7HAKRcFzBcC5A",
    "refreshToken": "9f65ff34-6cf3-4a05-8a7a-06f0e0414b52"
}
</code>

#### 1-3) Login By Refresh Token: 
##### HTTP Mehtod: POST
##### URL: /users/loginByRefreshToken
##### Request Body:

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Açıklama</th>
      <th>Örnek</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>id</td>
      <td>string (UUID)</td>
      <td>Kullanıcının benzersiz kimliği</td>
      <td>7a508887-758b-4533-a1c7-e479c2e0d832</td>
    </tr>
    <tr>
      <td>token</td>
      <td>string (UUID)</td>
      <td>Yeni access token almak için kullanılan refresh token</td>
      <td>1162841d-5832-43f2-b16e-0fdc0ee2c0cb</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği:
<code>
{
 "id": "7a508887-758b-4533-a1c7-e479c2e0d832",
 "token": "1162841d-5832-43f2-b16e-0fdc0ee2c0cb"
}
</code>

 ##### Hatalar

 <table>
  <thead>
    <tr>
      <th>Kod</th>
      <th>Açıklama</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>404</td>
      <td>User not found!!</td>
    </tr>
    <tr>
      <td>400</td>
      <td>The refresh token is invalid or expired</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

##### Response Body
<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Açıklama</th>
      <th>Örnek</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>accessToken</td>
      <td>string (JWT)</td>
      <td>Kullanıcının yetkilendirilmiş istek yapabilmesi için kullanılan geçerli JWT token</td>
      <td>eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9...</td>
    </tr>
    <tr>
      <td>refreshToken</td>
      <td>string (UUID)</td>
      <td>Access token süresi dolduğunda yeni token almak için kullanılan refresh token</td>
      <td>9f65ff34-6cf3-4a05-8a7a-06f0e0414b52</td>
    </tr>
  </tbody>
</table>

<code>
{
    "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4MTcwMzkxYS01YjE4LTQ1YTktOGE5Yi00MmE3N2YyYzUyZjMiLCJhdWQiOlsiZ2F0ZXdheS5hcGkiLCJwcm9kdWN0LmFwaSIsInVzZXIuYXBpIl0sImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiN2E1MDg4ODctNzU4Yi00NTMzLWExYzctZTQ3OWMyZTBkODMyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTc3NDYxNjc3MCwiZXhwIjoxNzc0NjE3NjcwLCJpc3MiOiJodHRwOi8vYXV0aHNlcnZpY2UuYXBpIn0.veZr1xOyqQo6lFviQlsRmJ52dgq8ma7HAKRcFzBcC5A",
    "refreshToken": "9f65ff34-6cf3-4a05-8a7a-06f0e0414b52"
}
</code>

#### 1-3) Delete User: 
##### HTTP Mehtod: DELETE
##### URL: /users/delete
##### Request Header:

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
      <th>Örnek</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Authorization</td>
      <td>Evet</td>
      <td>JWT access token, "Bearer " öneki ile gönderilmeli</td>
      <td>Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</td>
    </tr>
  </tbody>
</table>


##### Hatalar

 <table>
  <thead>
    <tr>
      <th>Kod</th>
      <th>Açıklama</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>404</td>
      <td>User not found!!</td>
    </tr>
    <tr>
      <td>401</td>
      <td>Unauthorized</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>
