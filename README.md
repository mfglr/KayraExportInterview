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
  <b> Microsoft Identity Framework, JWT:</b> Kullanıcı Üyelik sistemi, kimlik doğrulama ve token üretimi
</p>
<p>
  <b>Serilog, Serilog.Sinks.RabbitMQ:</b> Json formatında log üretimi ve RabbitMQ' ya gönderilmesi
</p>
<p>
  <b>MassTransit:</b> Event Publisher ile Consumer, Outbox-Inbox patttern ve Retry Strategilerinin uygulanması için kullanıldı.
</p>
<p>
  <b>MediatR</b>: CQRS pattern için command ve query işlemlerinin mediator aracılığıyla ayrıştırılması ve yönetilmesi;
  CommandHandler ve QueryHandler implementasyonlarıyla servisler arası loose coupling sağlanması için kullanıldı.
</p>
  
# Kurulum

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
