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

### 1️⃣ Repoyu klonla
<code>git clone https://github.com/mfglr/KayraExportInterview.git</code>

### 2️⃣ Proje dizinine gir
<code>cd KayraExportInterview</code>

### 3️⃣ Prod branch'ini indir ve geçiş yap
<p>
  <code>git fetch origin</code>
</p>
<p>
  <code>git checkout -b prod/v1.0.0 origin/prod/v1.0.0</code>
</p>

### 4️⃣ Docker Compose ile build ve çalıştır (arka planda)
<code>docker-compose up --build -d</code>

### 5️⃣ Servis loglarını takip etmek istersen
<code>docker-compose logs -f</code>
