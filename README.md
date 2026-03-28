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
    <b>Log Mikro Servis Ve Log Listener Worker:</b> Log Listener worker servisi, Auth, Gateway ve Product mikro servislerinden gelen logları RabbitMQ aracılığıyla asenkron olarak dinler, merkezi bir şekilde toplar ve analiz edilebilir hâle getirir. Logların depolanması için Elasticsearch kullanılmaktadır. Ayrıca, Log Mikro Servis, Elasticsearch’e sorgu gönderen endpointler sağlayarak logların görüntülenmesini mümkün kılar.
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
<p>
 <b>Container & Orkestrasyon:</b> Docker, Docker Compose
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
  <b>Outbox Pattern:</b> Veri tabanı işlemleri ile mesaj yayınlama süreçlerinin tutarlı bir şekilde yürütülmesi için kullanıldı; event’ler önce veritabanındaki outbox tablosuna yazıldı, ardından güvenilir şekilde message broker’a iletilerek eventual consistency sağlandı.
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

<p>
 <b>Onion Architecture</b>: Onion Architecture, uygulamanın domain (iş mantığı) katmanını merkeze alarak, tüm dış bağımlılıkları çevreleyen katmanlarla izole eden bir mimari yaklaşımdır. Bu sayede domain bağımsız kalır, test edilebilirlik artar ve altyapı veya UI değişiklikleri domain’i etkilemez. Katmanlar içten dışa doğru genellikle Domain, Application, Infrastructure, Presentation şeklinde organize edilir.
</p>
  
## Kurulum

<b>Repoyu klonla</b>
```cmd
 git clone https://github.com/mfglr/KayraExportInterview.git
```

<b>Proje dizinine gir</b>
```cmd
cd KayraExportInterview
```

<b>Prod branch'ini indir ve geçiş yap</b>
```cmd
git fetch origin
git checkout -b prod/v1.0.0 origin/prod/v1.0.0
```

<b>Docker Compose ile build ve çalıştır</b>
```cmd
docker-compose up --build -d
```

# PRODUCT SERVICE

<p>
 Product Service, ürünlerin yönetimi ve sorgulanmasını sağlayan mikroservistir. CRUD işlemleri, asenkron event handling ve caching ile güvenilir ve ölçeklenebilir bir altyapı sunar.
</p>

## Product Domain

| Domain Nesnesi         | Açıklama |
|------------------------|----------|
| **Product**            | Aggregate root. Product' ın tüm bilgilerini kapsar. |
| **Currency**           | Value Object. Ürün fiyatlarının para birimini temsil eder (TRY, USD, EUR). |
| **ProductDescription** | Value Object.Ürünün açıklama ve detay bilgilerini tutar. |
| **ProductPrice**       | Value Object. Ürünün fiyat bilgisi; Currency ile ilişkilidir. |
| **ProductTitle**       | Value Object. Ürünün başlık bilgisi. |
| **IProductRepository** | Repository. Product aggregate’ini veri kaynağından almak ve kaydetmek için kullanılan interface. |


<p>
 IProductRepository arayüzü sayesinde veri erişim katmanını soyutladım. Bu sayede veri tabanı teknolojisinde yapılacak değişiklikler, arayüzü kullanan servisler veya diğer bağımlı sınıfları etkilemeden uygulanabilir; böylece sistemin esnekliği ve sürdürülebilirliği artar.
</p>

### Neden "ProductDescription", "ProductPrice", "ProductTitle" primitif değil value object?
<p>
 Value Object’ler sayesinde domain kuralları ve validasyonlar tek bir merkezi noktada toplanabilir. Örneğin, her metin “ProductTitle” olamaz; bir metnin “ProductTitle” olarak kabul edilebilmesi için uzunluğunun 3 ile 256 karakter arasında olması gerekir. Bu kural tüm domain’de geçerlidir. Dolayısıyla, “ProductTitle” kullanımının olduğu her yerde tekrar tekrar validasyon yapmak yerine, bir Value Object tanımlayarak bu kuralların otomatik olarak uygulanmasını sağlarız.
</p>

#### ProductTitle Value Object Örneği

Aşağıda, `ProductTitle` Value Object sınıfının C# implementasyonu yer almaktadır. Bu sınıf, product başlığının domain kurallarına uygun olmasını sağlar; başlık boş olamaz ve uzunluğu 3 ile 256 karakter arasında olmalıdır.

```csharp
public class ProductTitle 
{
    private static readonly int _minLength = 3;
    private static readonly int _maxLength = 256;

    public string Value { get; private set; }

    public ProductTitle(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidTitleException("Product title cannot be empty.");
        
        if (value.Length < _minLength || value.Length > _maxLength)
            throw new InvalidTitleException($"Product title must be between {_minLength} and {_maxLength} characters.");

        Value = value;
    }
}
```


## Product Application

<p>
 Application katmanında, MediatR aracılığıyla tanımlanan request/query ve bunların karşılık gelen handler sınıfları ile use case’ler yürütülmektedir. Bu use case’lerde çalışması gereken ortak operasyonlar (ör. transaction commit) IPipeline arayüzü üzerinden merkezi olarak uygulanmıştır.

Tüm command’lerde, yani veritabanının state’ini değiştiren use case’lerde, yapılan değişikliklerin diğer mikroservislere iletilebilmesi için bir event publish edilmesi gerekir. Ancak veritabanı değişikliği ile event’in message broker’a gönderilmesi deterministik değildir: veritabanı değişikliği başarılı olurken event broker’a iletilemeyebilir veya tam tersi bir durum gerçekleşebilir.

Bu problemi çözmek için Outbox Pattern uygulanmıştır. Event doğrudan message broker’a gönderilmek yerine, aynı transaction ile veritabanına kaydedilir. Böylece, transaction commit edildikten sonra event’in güvenli bir şekilde publish edilmesi sağlanır.
</p>

### Use Cases

| Type    | Use Case                | Açıklama                          |
|---------|------------------------|----------------------------------|
| Command | Create Product         | Yeni ürün oluşturur              |
| Command | Update Product         | Mevcut ürünü günceller           |
| Command | Delete Product         | Ürünü siler                      |
| Query   | Get Product By Id      | ID’ye göre ürün getirir          |
| Query   | Get All Products       | Tüm ürünleri listeler            |
| Query   | Search Products        | Ürünler üzerinde arama yapar     |

Use case’ler bu şekilde ayrılarak CQRS prensibine uygun bir yapı sağlanmıştır. Command’ler state değişikliğine neden olurken, Query’ler yalnızca veri okuma işlemlerini gerçekleştirir. Aşağıda örnek olarak bir Create Product (Command) ve Get All Products (Query) use case’i gösterilmiştir.

### CreateProductCommandHandler

```csharp
internal class CreateProductCommandHandler(
    IProductRepository repository,
    IPublishEndpoint publishEndpoint,
    CreateProductCommandMapper mapper,
    IProductCacheService cacheService,
    IAuthService authService
) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var userId = authService.UserId;
        var title = new ProductTitle(request.Title);
        var description = new ProductDescription(request.Description);
        var currency = new Currency(request.Currency);
        var price = new ProductPrice(request.Price, currency);
        var product = new Product(userId, request.CategoryId, title, description, price);

        await repository.CreateAsync(product, cancellationToken);

        var @event = mapper.Map(product);
        await publishEndpoint.Publish(@event, cancellationToken);

        await cacheService.UpdateProductListVersion();

        return new(product.Id);
    }
}
```

### UnitOfWorkPipelineBehaviour

<p>
 UnitOfWorkPipelineBehaviour, MediatR pipeline’ında Command işlemleri sırasında transaction yönetimini merkezi olarak ele alır ve işlem başarılı olduğunda commit yapar.
</p>


```csharp
internal class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next(cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return response;
    }
}
```

### GetAllProductsQueryHandler

GetAllProductsQueryHandler, ürünleri öncelikle cache üzerinden almaya çalışır; eğer cache’de veri bulunamazsa veritabanına sorgu atarak sonucu elde eder. Caching hakkında daha fazla bilgi için [Product List Caching ve Cache Invalidation Stratejisi](#product-list-caching-ve-cache-invalidation-stratejisi)' ne bakınız.

```csharp
internal class GetAllProductsQueryHandler(
    IProductRepository repository,
    ProductQueryResponseMapper mapper,
    IProductCacheService cacheService
) : IRequestHandler<GetAllProductsQueryRequest, List<ProductQueryResponse>>
{
    public async Task<List<ProductQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var dtos = await cacheService.GetAsync(request.PageSize, request.Cursor);
        if (dtos != null) return dtos;

        var products = await repository.GetAllAsync(request.Cursor, request.PageSize, cancellationToken);
        dtos = [..products.Select(mapper.Map)];
        await cacheService.UpsertAsync(request.PageSize, request.Cursor, dtos);
        return dtos;
    }
}
```

### Handler Class' ları Neden Interface (IProductCacheService, IProductRepository, IAuthService, IPublishEndpoint ...) Inject ediyor? (Dependency Inversion)

Handler sınıfları (IProductCacheService, IProductRepository, IAuthService, IPublishEndpoint vb.) somut implementasyonlar yerine interface’ler üzerinden bağımlılık alır. Bu yaklaşım, Dependency Inversion Principle (DIP) gereğidir.

| Kavram                    | Açıklama |
|---------------------------|----------|
| **Esneklik**              | Altyapı değişiklikleri (örneğin Redis yerine başka bir cache, RabbitMQ yerine farklı bir broker) handler kodunu etkilemeden değiştirilebilir. |
| **Test Edilebilirlik**    | Interface’ler sayesinde mock veya stub implementasyonlar kolayca yazılarak unit test yapılabilir. |

Bu sayede sistem daha modüler, sürdürülebilir ve genişletilebilir bir yapıya sahip olur.

### Önemli Not
Onion Architecture ile geliştirilen projede, Domain ve Application katmanları dış katmanlardan bağımsız olacak şekilde tasarlanmıştır. Bu sayede Infrastructure veya Presentation katmanlarında yapılan değişiklikler, bu katmanları doğrudan etkilemez.

## Product Infrastructure

Bu katman, uygulamanın dış bağımlılıklarını ve teknik implementasyonlarını içerir. Veri erişimi ve cache yönetimi altyapı işlemleri bu katmanda gerçekleştirilir.

EF Core kullanılarak SQL Server üzerinden veri erişimi sağlanır.
StackExchange.Redis ile cache işlemleri yönetilir.
Repository implementasyonları, Application katmanında tanımlanan interface’leri uygular.

Bu sayede Application ve Domain katmanları altyapı detaylarından izole edilerek daha modüler ve sürdürülebilir bir yapı elde edilir.

### Product List Caching ve Cache Invalidation Stratejisi

Bu projede ürün listeleme [Get All Products](#get-all-products) endpoint’i için Redis tabanlı cache mekanizması uygulanmıştır. Cache invalidation problemi, versioning (versiyonlama) yaklaşımı ile çözülmüştür.

 #### Amaç
  - Sık kullanılan ürün listeleme sorgularını cache’lemek</ol>
  - Veritabanı yükünü azaltmak</ol>
  - Tutarlı (stale olmayan) veri sunmak</ol>

#### Strateji: Version-Based Cache Invalidation
<p>
 Ürün listeleri cache’lenirken her cache key, global bir liste versiyonu ile birlikte oluşturulur.
</p>

<code>list:{pageSize}:{cursor}:{ProductListVersionKey}</code>

#### Cache Invalidation Mekanizması
<p>
 Aşağıdaki durumlarda cache manuel silinmez, bunun yerine versiyon artırılır:
 <ul>
  <li>Yeni ürün eklenmesi</li>
  <li>Ürün güncellenmesi</li>
  <li>Ürün silinmesi</li>
</ul>
</p>

```csharp
 public Task UpdateProductListVersion() => database.StringIncrementAsync(_productListVersionKey, 1);
```

<p>
 Bu işlem, eski cache key’leri geçersiz hale getirir ve yeni isteklerde farklı bir version ile yeni cache oluşturulur.
</p>

#### Liste Çekme

<p>
 Önce Redis’ten mevcut ProductListVersionKey alınır. Daha sonra bu version ile page okunur. Page varsa gönderilir yoksa veritabanından okunur.
</p>

```csharp

 private async Task<long> GetProductListVersion()
 {
     var result = await database.StringGetAsync(_productListVersionKey);
     return result.HasValue ? (long)result : 0;
 }

 public async Task<List<ProductQueryResponse>?> GetAsync(int pageSize, DateTime? cursor)
 {
     var version = await GetProductListVersion();
     var id = Id(cursor, pageSize,version);
     var json = await database.StringGetAsync(id);
     return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<List<ProductQueryResponse>>(json.ToString());
 }
```

#### Trade-Off
<p>
 Eski cache verileri Redis’te kalmaya devam eder (memory trade-off). TTL eklenerek bu problem minimize edilebilir ya da clean-up worker yazılabilir.
</p>


# AUTH SERVICE

Auth Microservice, Product Microservice’te uygulanan temel mimari ve tasarım prensiplerini tekrar etmektedir; bu bölümde sadece Auth servisine özgü, farklı özellikler ve iş akışları üzerinde durulacaktır.

| Domain Nesnesi                  | Açıklama |
|--------------------------------|----------|
| **UserCreatorDomainService**    | Yeni kullanıcı oluşturma işlemlerini yöneten domain servisi. |
| **UserNameUpdaterDomainService**| Kullanıcı adını güncelleme işlemlerini yöneten domain servisi. |
| **User**                        | Aggregate root; kullanıcıya ait tüm bilgileri kapsar. |
| **Email**                       | Kullanıcının email bilgisi. |
| **Password**                    | Kullanıcının şifre bilgisi. |
| **RefreshToken**                | Kullanıcının refresh token bilgisi, oturum yönetimi için kullanılır. |
| **UserName**                     | Kullanıcının adı bilgisi. |

## Domain Service
Domain Service’ler, aggregate’lere ait olmayan fakat domain mantığıyla ilgili iş kurallarını kapsayan operasyonları yürütür.

### UserNameUpdaterDomainService
Bir kullanıcı adı benzersiz olmalıdır; bu bir domain kuralıdır. Dolayısıyla bir kullanıcının kullanıcı adı değiştirildiğinde, aynı username’in başka bir kullanıcıda olup olmadığı kontrol edilmelidir. Bu kontrol, farklı bir User aggregate’in state’ine ihtiyaç duyar ve genellikle IUserRepository aracılığıyla gerçekleştirilir.

```csharp
public class UserNameUpdaterDomainService(IUserRepository userRepository)
{
    public async Task UpdateAsync(User user, UserName userName, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistAsync(userName,cancellationToken))
            throw new UserNameAlreadyTakeException();

        user.UpdateUserName(userName);
    }
}
```

## Use Case

| Use Case                  | Açıklama |
|----------------------------|----------|
| **CreateUser**             | Yeni kullanıcı oluşturur. |
| **DeleteUser**             | Mevcut kullanıcıyı siler. |
| **Login**                  | Kullanıcı girişi yapar ve access token üretir. |
| **LoginByRefreshToken**    | Refresh token kullanarak yeni access token üretir. |
| **UpdateUserName**         | Kullanıcının kullanıcı adını günceller. |

## Access Token Üretimi

Access Token, bir kullanıcının veya servisin bir kaynağa erişim yetkisini temsil eden kısa ömürlü bir kimlik doğrulama bilgisidir. Modern web ve microservice mimarilerinde, özellikle JWT (JSON Web Token) formatında kullanılır. Access Token, sadece kimliği doğrulamakla kalmaz, aynı zamanda kullanıcının rol ve izin bilgilerini (claim) de taşır.

Token üretimi sırasında iki temel yaklaşım vardır: simetrik anahtar ve asimetrik anahtar kullanımı.

| Özellik | Simetrik Key (HMAC) | Asimetrik Key (Public/Private Key) |
|---------|-------------------|-----------------------------------|
| Anahtar sayısı | Tek anahtar (secret key) | İki anahtar: Private ve Public |
| Kullanım | Hem token imzalama hem doğrulama | Private key ile imzalama, Public key ile doğrulama |
| Güvenlik | Anahtar paylaşımı gerekir; kaybolursa tüm sistem etkilenir | Public key paylaşılabilir; private key gizli tutulur, daha güvenli dağıtım |
| Performans | Hızlı, hesaplama maliyeti düşük | Daha yavaş, hesaplama maliyeti yüksek |
| Örnek Algoritmalar | HMACSHA256, HMACSHA512 | RS256, ES256 |


Bu uygulamada HMAC-SHA256 algoritması ile imzalanmış simetrik anahtar (secret key) kullanılarak Access Token üretilmektedir. Token içindeki claim’ler, kullanıcının kimliği ve rollerine dair bilgileri içerir; böylece token doğrulandığında kullanıcının sisteme gerçekten ulaşıp ulaşmadığı ve hangi yetkilere sahip olduğu güvenli bir şekilde tespit edilebilir.


```csharp
internal class AccessTokenGenerator(ITokenOptions tokenOptions, UserManager<User> userManager) : IAccessTokenGenerator
{
    private async Task<List<Claim>> GetClaims(User user)
    {
        var roles = await userManager.GetRolesAsync(user);

        return [
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ..tokenOptions.Audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            .. roles.Select(role => new Claim(ClaimTypes.Role, role))
        ];
    }

    public async Task<string> GenerateAsync(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
        JwtSecurityToken jwtSecurityToken = new(
            issuer: tokenOptions.Issuer,
            expires: DateTime.Now.AddMinutes(tokenOptions.AccessTokenValidtyPeriod),
            notBefore: DateTime.Now,
            claims: await GetClaims(user),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
```

### Refresh Token Yönetimi

Uygulamada **Refresh Token** mekanizması, kullanıcıların uzun süreli oturumlarını güvenli bir şekilde yönetmek için kullanılır. Her `User` entity’si oluşturulduğunda veya giriş yaptığında, belirli bir süre geçerliliğe sahip **yeni bir Refresh Token** üretilir.

- **Benzersiz ve güvenli:** Refresh Token, **GUID** olarak oluşturulur ve **SHA256 ile hash’lenerek** veritabanında saklanır.
- **Geçerlilik kontrolü:** Token’ın süresi `ExpiredAt` ile takip edilir; süresi dolmuş token’lar geçersiz sayılır.
- **Doğrulama:** Kullanıcı token ile oturum açmaya çalıştığında, token `IsValid` metodu ile doğrulanır ve sadece geçerli, eşleşen token’lar kabul edilir.
- **Tek token politikası:** Yeni bir giriş veya token yenileme işleminde, önceki token’lar temizlenir ve yerine **yeni bir token** eklenir.

Bu yaklaşım, hem **güvenli** hem de **stateless authentication** ile uyumlu bir oturum yönetimi sağlar; token’lar kullanıcıya verilmeden önce hashlenir ve yalnızca hash veritabanında saklanır.

```csharp
public class RefreshToken
{
    public string Token { get; private set; } //not mapped
    public byte[] TokenHash { get; private set; }
    public DateTime ExpiredAt { get; private set; }

    //for ef core
    private RefreshToken() { }

    public RefreshToken(TimeSpan timeSpan)
    {
        Token = Guid.NewGuid().ToString();
        var bytes = Encoding.UTF8.GetBytes(Token);
        TokenHash = SHA256.HashData(bytes);
        
        ExpiredAt = DateTime.UtcNow.Add(timeSpan);
    }

    public bool IsValid(string token)
    {
        ArgumentNullException.ThrowIfNull(token, nameof(token));

        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = SHA256.HashData(bytes);
        return hash.SequenceEqual(TokenHash) && ExpiredAt > DateTime.UtcNow;
    }
}

public class User : IdentityUser
{
    ...

    public void Login(TimeSpan refreshTokenValidtyPeriod)
    {
        _refreshTokens.Clear();
        _refreshTokens.Add(new RefreshToken(refreshTokenValidtyPeriod));
    }

    public void LoginByRefreshToken(string token, TimeSpan refreshTokenValidtyPeriod)
    {
        if (!_refreshTokens.Any(x => x.IsValid(token)))
            throw new InvalidOrExpiredRefreshTokenException();

        _refreshTokens.Clear();
        _refreshTokens.Add(new RefreshToken(refreshTokenValidtyPeriod));
    }
   ...
}
```

# API GATEWAY

API Gateway’de YARP (Yet Another Reverse Proxy) kullanılmıştır. Gateway, tüm servisler için merkezi rate limiting uygular ve authentication/authorization işlemlerini burada gerçekleştirir. Gelen isteklerdeki Access Token doğrulaması da API Gateway üzerinde yapılmaktadır.

## Authentication Konfigrasyonu

JWT tabanlı authentication, API Gateway’de yapılandırılmıştır. AddAuthentication ve AddJwtBearer ile gateway, gelen isteklerde Access Token doğrulaması yapar; Authority ve Audience değerleri token’ın kaynağını ve hedefini belirlerken, TokenValidationParameters ile token’ın imzası, süresi ve geçerliliği kontrol edilir.

```csharp
services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            options.Authority = authOptions.Issuer;
            options.Audience = authOptions.Audience;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new()
            {
                IssuerSigningKey = securityKey,
                ValidIssuer = authOptions.Issuer,
                ValidAudience = authOptions.Audience,

                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    );
```

## Authorization Konfigrasyonu

API Gateway’de authorization da JWT tabanlı olarak yapılandırılmıştır. AddAuthorization ile “user” adında bir policy tanımlanır; bu policy, yalnızca doğrulanmış (authenticated) kullanıcıların ve "user" rolüne sahip kullanıcıların erişimine izin verir.

```csharp
services
    .AddAuthorization(
        options =>
        {
            options
                .AddPolicy(
                    "user",
                    p =>
                    {
                        p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        p.RequireAuthenticatedUser();
                        p.RequireRole("user");
                    }
                );
        }
    );
```

## Rate Limit Konfigrasyonu

API Gateway’de merkezi rate limiting uygulanmıştır. AddRateLimiter ile her kullanıcı veya IP adresi için sabit pencere (Fixed Window) limiti tanımlanır; limit aşıldığında istekler 429 (Too Many Requests) hatası ile reddedilir. Kullanıcıyı belirlemek için JWT claim’leri veya IP adresi kullanılır ve limitleme değerleri (PermitLimit, Window, QueueLimit) konfigürasyondan okunur. Bu yapı, servisleri aşırı yükten korur ve isteklerin kontrollü şekilde işlenmesini sağlar.

```csharp
services
    .AddRateLimiter(options =>
    {
        options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
            (context) =>
            {
                var key =
                    context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                    context.Connection.RemoteIpAddress?.ToString() ??
                    "anonymous";

                return RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = int.Parse(configuration["RateLimit:PermitLimit"]!),
                    Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimit:Window"]!)),
                    QueueLimit = int.Parse(configuration["RateLimit:QueueLimit"]!),
                    AutoReplenishment = true
                });
            }
        );

        options.OnRejected = async (context, token) =>
        {
            context.HttpContext.Response.StatusCode = 429;
            await context.HttpContext.Response.WriteAsJsonAsync(new { Message = "Rate limit exceeded" }, token);
        };

    });
```

# LOG SERVICE ve LOG LISTENER WORKER

<b>Log Listener Worker:</b> Uygulamalar tarafından Serilog aracılığıyla RabbitMQ’ya gönderilen log event’lerini dinler ve bu kayıtları Elasticsearch’e kaydeder. Böylece log verileri merkezi ve aramaya hazır hâle gelir.
<b>Log Service:</b> Elasticsearch’e kaydedilen loglar üzerinde arama ve filtreleme işlemleri yapmak için RESTful endpoint’ler sağlar.

| Domain Nesnesi      | Açıklama |
|--------------------|----------|
| **Exception**      | Uygulamada meydana gelen hata veya istisnaları temsil eder. |
| **ILogRepository** | Log kayıtlarının veri kaynağına (database, dosya, vs.) yazılması ve okunması için kullanılan interface. |
| **Log**            | Uygulama içi önemli olayları, hata ve bilgi mesajlarını merkezi olarak tutan nesne. |

## Use Cases

| Use Case       | Açıklama |
|----------------|----------|
| **CreateLog**  | Tek bir log kaydını oluşturur. |
| **CreateLogs** | Birden fazla log kaydını toplu olarak oluşturur. |
| **SearchLogs** | Log kayıtları üzerinde arama yapar ve filtreleme sağlar. |


## Elastic Search Konfigrasyon

Log microservice’de Elasticsearch entegrasyonu ServiceRegistration sınıfı ile yapılandırılmıştır. AddElacticSearch extension metodu, konfigürasyondan alınan host bilgisi ile ElasticsearchClient oluşturur ve servis koleksiyonuna singleton olarak ekler. Ayrıca ILogRepository interface’i, log kayıtlarını Elasticsearch’e yazmak ve okumak için LogRepository implementasyonu ile scoped olarak eklenir. Bu sayede log verileri merkezi bir şekilde depolanır ve erişilebilir hâle gelir.

```csharp
internal static class ServiceRegistration
{
    public static IServiceCollection AddElacticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.GetSection(nameof(ElasticSearchOptions))!;
        var clientSettings = new ElasticsearchClientSettings(new Uri(option["Host"]!));

        return services
            .AddSingleton(new ElasticsearchClient(clientSettings))
            .AddScoped<ILogRepository, LogRepository>();
    }
        
}
```



# API Dokümantasyonu

### Users

#### Create User
##### HTTP Mehtod: POST
##### URL: /users/create
##### Request Body

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

##### Request Body Örneği

```json
{
 "email": "aliveli@example.com",
 "password": "123456"
}
```

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
      <td>429</td>
      <td>Rate limit exceeded</td>
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

```json
{
    "id": "7a508887-758b-4533-a1c7-e479c2e0d832",
    "token": {
        "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzNTdkNTBlNS0wZmUxLTRjYzAtODYzNC03NTFkZGU2ZGJjYjEiLCJhdWQiOlsiZ2F0ZXdheS5hcGkiLCJwcm9kdWN0LmFwaSIsInVzZXIuYXBpIl0sImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiN2E1MDg4ODctNzU4Yi00NTMzLWExYzctZTQ3OWMyZTBkODMyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTc3NDYxNjI3NSwiZXhwIjoxNzc0NjE3MTc1LCJpc3MiOiJodHRwOi8vYXV0aHNlcnZpY2UuYXBpIn0.slVrSJRZU1ZmGYsKx7gTUSilm2CFEofeKGjmdktDBnY",
        "refreshToken": "1162841d-5832-43f2-b16e-0fdc0ee2c0cb"
    }
}
```

#### Update User Name
##### HTTP Mehtod: PUT
##### URL: /users/updateUserName
##### Request Body

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
      <td>userName</td>
      <td>string</td>
      <td>Evet</td>
      <td>Kullanıcının e-posta adresi</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği

```json
{
 "userName": "mfgglr"
}
```

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
      <td>401</td>
      <td>Unauthorized</td>
    </tr>
   <tr>
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>


#### Login
##### HTTP Mehtod: POST
##### URL: /users/login
##### Request Body

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

##### Request Body Örneği

```json
{
 "key": "aliveli@example.com",
 "password": "123456"
}
```
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
      <td>429</td>
      <td>Rate limit exceeded</td>
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

```json
{
    "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4MTcwMzkxYS01YjE4LTQ1YTktOGE5Yi00MmE3N2YyYzUyZjMiLCJhdWQiOlsiZ2F0ZXdheS5hcGkiLCJwcm9kdWN0LmFwaSIsInVzZXIuYXBpIl0sImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiN2E1MDg4ODctNzU4Yi00NTMzLWExYzctZTQ3OWMyZTBkODMyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTc3NDYxNjc3MCwiZXhwIjoxNzc0NjE3NjcwLCJpc3MiOiJodHRwOi8vYXV0aHNlcnZpY2UuYXBpIn0.veZr1xOyqQo6lFviQlsRmJ52dgq8ma7HAKRcFzBcC5A",
    "refreshToken": "9f65ff34-6cf3-4a05-8a7a-06f0e0414b52"
}
```

#### Login By Refresh Token
##### HTTP Mehtod: POST
##### URL: /users/loginByRefreshToken
##### Request Body

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

##### Request Body Örneği

```json
{
 "id": "7a508887-758b-4533-a1c7-e479c2e0d832",
 "token": "1162841d-5832-43f2-b16e-0fdc0ee2c0cb"
}
```

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
      <td>429</td>
      <td>Rate limit exceeded</td>
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

```json
{
    "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4MTcwMzkxYS01YjE4LTQ1YTktOGE5Yi00MmE3N2YyYzUyZjMiLCJhdWQiOlsiZ2F0ZXdheS5hcGkiLCJwcm9kdWN0LmFwaSIsInVzZXIuYXBpIl0sImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiN2E1MDg4ODctNzU4Yi00NTMzLWExYzctZTQ3OWMyZTBkODMyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTc3NDYxNjc3MCwiZXhwIjoxNzc0NjE3NjcwLCJpc3MiOiJodHRwOi8vYXV0aHNlcnZpY2UuYXBpIn0.veZr1xOyqQo6lFviQlsRmJ52dgq8ma7HAKRcFzBcC5A",
    "refreshToken": "9f65ff34-6cf3-4a05-8a7a-06f0e0414b52"
}
```

#### Delete User
##### HTTP Mehtod: DELETE
##### URL: /users/delete
##### Request Header

Bu endpoint’e erişebilmek için user rolüne sahip bir access token gereklidir. Eğer kayıtlı bir kullanıcıysanız token almak için [login by refresh token](#login-by-refresh-token) ya da [login](#login) endpoint i kullanın. Yeni bir kullanıcı oluşturmak için [create user](#create-user) endpoint' ini kullanın.

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
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>


### Products

#### Create Product
##### HTTP Mehtod: POST
##### URL: /products/create
##### Request Header

Bu endpoint’e erişebilmek için user rolüne sahip bir access token gereklidir. Eğer kayıtlı bir kullanıcıysanız token almak için [login by refresh token](#login-by-refresh-token) ya da [login](#login) endpoint i kullanın. Yeni bir kullanıcı oluşturmak için [create user](#create-user) endpoint' ini kullanın.

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

##### Request Body

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
      <th>Örnek</th>
      <th>Alabileceği Değerler</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>categoryId</td>
      <td>string (UUID)</td>
      <td>Evet</td>
      <td>Ürünün ait olduğu kategori ID'si</td>
      <td>79b36246-3c43-40c1-8695-13425c171850</td>
      <td>Herhangi geçerli UUID</td>
    </tr>
    <tr>
      <td>title</td>
      <td>string</td>
      <td>Evet</td>
      <td>Ürünün başlığı veya adı</td>
      <td>test</td>
      <td>Minimum 3 karakter, maksimum 256 karakter</td>
    </tr>
    <tr>
      <td>description</td>
      <td>string</td>
      <td>Hayır</td>
      <td>Ürün açıklaması</td>
      <td>test test test</td>
      <td>Minimum 10, maksimum 2000 karakter</td>
    </tr>
    <tr>
      <td>price</td>
      <td>decimal</td>
      <td>Evet</td>
      <td>Ürünün fiyatı</td>
      <td>15</td>
      <td>> 0</td>
    </tr>
    <tr>
      <td>Currency</td>
      <td>string</td>
      <td>Evet</td>
      <td>Fiyatın para birimi</td>
      <td>TRY</td>
      <td>TRY, USD, EUR</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği:

```json
{
    "categoryId": "79b36246-3c43-40c1-8695-13425c171850",
    "title": "test",
    "description": "test test test",
    "price": "15",
    "Currency": "TRY"
}
```

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
      <td>401</td>
      <td>Unauthorized</td>
    </tr>
   <tr>
      <td>403</td>
      <td>Forbidden</td>
    </tr>
    <tr>
      <td>400</td>
      <td>Product title must be between 3 and 256 characters.</td>
    </tr>
   <tr>
      <td>400</td>
      <td>Product description must be between 10 and 2000 characters.</td>
    </tr>
   <tr>
      <td>400</td>
      <td>Price must be greater than zero.</td>
    </tr>
   <tr>
      <td>400</td>
      <td>Invalid currency.</td>
    </tr>
   <tr>
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

#### Update Product
##### HTTP Mehtod: PUT
##### URL: /products/update
##### Request Header: Bu endpoint’e erişebilmek için user rolüne sahip bir access token gereklidir.

Bu endpoint’e erişebilmek için user rolüne sahip bir access token gereklidir. Eğer kayıtlı bir kullanıcıysanız token almak için [login by refresh token](#login-by-refresh-token) ya da [login](#login) endpoint i kullanın. Yeni bir kullanıcı oluşturmak için [create user](#create-user) endpoint' ini kullanın.

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

##### Request Body:

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tür</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
      <th>Örnek</th>
      <th>Alabileceği Değerler</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>title</td>
      <td>string</td>
      <td>Evet</td>
      <td>Ürünün başlığı veya adı</td>
      <td>test</td>
      <td>Minimum 3 karakter, maksimum 256 karakter</td>
    </tr>
    <tr>
      <td>description</td>
      <td>string</td>
      <td>Hayır</td>
      <td>Ürün açıklaması</td>
      <td>test test test</td>
      <td>Minimum 10, maksimum 2000 karakter</td>
    </tr>
    <tr>
      <td>price</td>
      <td>decimal</td>
      <td>Evet</td>
      <td>Ürünün fiyatı</td>
      <td>15</td>
      <td>> 0</td>
    </tr>
    <tr>
      <td>Currency</td>
      <td>string</td>
      <td>Evet</td>
      <td>Fiyatın para birimi</td>
      <td>TRY</td>
      <td>TRY, USD, EUR</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği:

```json
{
    "title": "test",
    "description": "test test test",
    "price": "15",
    "Currency": "TRY"
}
```

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
      <td>401</td>
      <td>Unauthorized</td>
    </tr>
   <tr>
      <td>403</td>
      <td>Forbidden</td>
    </tr>
    <tr>
      <td>400</td>
      <td>Product title must be between 3 and 256 characters.</td>
    </tr>
   <tr>
      <td>400</td>
      <td>Product description must be between 10 and 2000 characters.</td>
    </tr>
   <tr>
      <td>400</td>
      <td>Price must be greater than zero.</td>
    </tr>
   <tr>
      <td>400</td>
      <td>Invalid currency.</td>
    </tr>
   <tr>
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>


#### Delete Product
##### HTTP Mehtod: DELETE
##### URL: /products/delete/{product-id}

<table>
  <thead>
    <tr>
      <th>Parametre Adı</th>
      <th>Tipi</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
      <th>Örnek Değer</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>product-id</td>
      <td>UUID / string</td>
      <td>Evet</td>
      <td>Silinecek ürünün benzersiz kimliği (ID)</td>
      <td>79b36246-3c43-40c1-8695-13425c171850</td>
    </tr>
  </tbody>
</table>

##### Request Header: Bu endpoint’e erişebilmek için user rolüne sahip bir access token gereklidir.

Bu endpoint’e erişebilmek için user rolüne sahip bir access token gereklidir. Eğer kayıtlı bir kullanıcıysanız token almak için [login by refresh token](#login-by-refresh-token) ya da [login](#login) endpoint i kullanın. Yeni bir kullanıcı oluşturmak için [create user](#create-user) endpoint' ini kullanın.

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
      <td>401</td>
      <td>Unauthorized</td>
    </tr>
   <tr>
      <td>403</td>
      <td>Forbidden</td>
    </tr>
   <tr>
      <td>404</td>
      <td>Product not found!</td>
    </tr>
   <tr>
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

#### Get Product By Id
##### HTTP Mehtod: GET
##### URL: /products/query/getById/{product-id}

<table>
  <thead>
    <tr>
      <th>Parametre Adı</th>
      <th>Tipi</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
      <th>Örnek Değer</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>product-id</td>
      <td>UUID / string</td>
      <td>Evet</td>
      <td>Okunan ürünün benzersiz kimliği (ID)</td>
      <td>79b36246-3c43-40c1-8695-13425c171850</td>
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
      <td>Product not found!</td>
    </tr>
   <tr>
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>


##### Request Body

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tipi</th>
      <th>Açıklama</th>
      <th>Örnek Değer</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>id</td>
      <td>UUID / string</td>
      <td>Ürünün benzersiz kimliği</td>
      <td>019d2fab-a860-774c-a3c0-9ff1bc7de95b</td>
    </tr>
    <tr>
      <td>createdAt</td>
      <td>string (ISO 8601)</td>
      <td>Ürünün oluşturulma tarihi</td>
      <td>2026-03-27T14:21:12.1607198Z</td>
    </tr>
    <tr>
      <td>updatedAt</td>
      <td>string / null</td>
      <td>Ürünün güncellenme tarihi (null ise güncellenmemiş)</td>
      <td>null</td>
    </tr>
    <tr>
      <td>categoryId</td>
      <td>UUID / string</td>
      <td>Ürünün ait olduğu kategori ID’si</td>
      <td>79b36246-3c43-40c1-8695-13425c171850</td>
    </tr>
    <tr>
      <td>title</td>
      <td>string</td>
      <td>Ürünün başlığı</td>
      <td>test</td>
    </tr>
    <tr>
      <td>description</td>
      <td>string</td>
      <td>Ürünün açıklaması</td>
      <td>test test test</td>
    </tr>
    <tr>
      <td>price.price</td>
      <td>decimal</td>
      <td>Ürünün fiyatı</td>
      <td>0.01</td>
    </tr>
    <tr>
      <td>price.currency</td>
      <td>string</td>
      <td>Fiyatın para birimi</td>
      <td>TRY</td>
    </tr>
  </tbody>
</table>


##### Request Body Örneği

```json
 {
    "id": "019d2fab-a860-774c-a3c0-9ff1bc7de95b",
    "createdAt": "2026-03-27T14:21:12.1607198Z",
    "updatedAt": null,
    "categoryId": "79b36246-3c43-40c1-8695-13425c171850",
    "title": "test",
    "description": "test test test",
    "price": {
        "price": 0.01,
        "currency": "TRY"
    }
}
```


#### Get All Products
##### HTTP Mehtod: GET
##### URL: /products/query/getAll?pageSize={page-size:int}&cursor={cursor:guid}

<table>
  <thead>
    <tr>
      <th>Parametre Adı</th>
      <th>Tipi</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
      <th>Örnek Değer</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>page-size</td>
      <td>integer</td>
      <td>Hayır</td>
      <td>Sayfa başına getirilecek ürün sayısı. Varsayılan değeri 20 olabilir.</td>
      <td>20</td>
    </tr>
    <tr>
      <td>cursor</td>
      <td>datetime string</td>
      <td>Hayır</td>
      <td>Sayfalama için kullanılacak başlangıç zamanı. Önceki cevaptaki son ürünün Timestamp’ine göre veriler çekilir.</td>
      <td>2026-03-24T22:53:23.8699511</td>
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
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

##### Request Body

<p>
 Aşağıdaki tabloda verilen alanlara sahip obje listesi döner.
</p>

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tipi</th>
      <th>Açıklama</th>
      <th>Örnek Değer</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>id</td>
      <td>UUID / string</td>
      <td>Ürünün benzersiz kimliği</td>
      <td>019d2fab-a860-774c-a3c0-9ff1bc7de95b</td>
    </tr>
    <tr>
      <td>createdAt</td>
      <td>string (ISO 8601)</td>
      <td>Ürünün oluşturulma tarihi</td>
      <td>2026-03-27T14:21:12.1607198</td>
    </tr>
    <tr>
      <td>updatedAt</td>
      <td>string / null</td>
      <td>Ürünün güncellenme tarihi (null ise güncellenmemiş)</td>
      <td>null</td>
    </tr>
    <tr>
      <td>categoryId</td>
      <td>UUID / string</td>
      <td>Ürünün ait olduğu kategori ID’si</td>
      <td>79b36246-3c43-40c1-8695-13425c171850</td>
    </tr>
    <tr>
      <td>title</td>
      <td>string</td>
      <td>Ürünün başlığı</td>
      <td>test</td>
    </tr>
    <tr>
      <td>description</td>
      <td>string</td>
      <td>Ürünün açıklaması</td>
      <td>test test test</td>
    </tr>
    <tr>
      <td>price.price</td>
      <td>decimal</td>
      <td>Ürünün fiyatı</td>
      <td>0.0100</td>
    </tr>
    <tr>
      <td>price.currency</td>
      <td>string</td>
      <td>Fiyatın para birimi</td>
      <td>TRY</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği

```json
 [
    {
        "id": "019d2fab-a860-774c-a3c0-9ff1bc7de95b",
        "createdAt": "2026-03-27T14:21:12.1607198",
        "updatedAt": null,
        "categoryId": "79b36246-3c43-40c1-8695-13425c171850",
        "title": "test",
        "description": "test test test",
        "price": {
            "price": 0.0100,
            "currency": "TRY"
        }
    }
]
```

#### Search Products
##### HTTP Mehtod: GET
##### URL: /products/query/Search?key={key:string}&pageSize={page-size:int}&cursor={cursor:guid}

<table>
  <thead>
    <tr>
      <th>Parametre Adı</th>
      <th>Tipi</th>
      <th>Zorunlu</th>
      <th>Açıklama</th>
      <th>Örnek Değer</th>
    </tr>
  </thead>
  <tbody>
   <tr>
      <td>key</td>
      <td>string</td>
      <td>Hayır</td>
      <td>Title ve description' da aranacak kelime</td>
      <td>Test</td>
    </tr>
    <tr>
      <td>page-size</td>
      <td>integer</td>
      <td>Hayır</td>
      <td>Sayfa başına getirilecek ürün sayısı. Varsayılan değeri 20 olabilir.</td>
      <td>20</td>
    </tr>
    <tr>
      <td>cursor</td>
      <td>datetime string</td>
      <td>Hayır</td>
      <td>Sayfalama için kullanılacak başlangıç zamanı. Önceki cevaptaki son ürünün Timestamp’ine göre veriler çekilir.</td>
      <td>2026-03-24T22:53:23.8699511</td>
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
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

##### Request Body

<p>
 Aşağıdaki tabloda verilen alanlara sahip obje listesi döner.
</p>

<table>
  <thead>
    <tr>
      <th>Alan</th>
      <th>Tipi</th>
      <th>Açıklama</th>
      <th>Örnek Değer</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>id</td>
      <td>UUID / string</td>
      <td>Ürünün benzersiz kimliği</td>
      <td>019d2fab-a860-774c-a3c0-9ff1bc7de95b</td>
    </tr>
    <tr>
      <td>createdAt</td>
      <td>string (ISO 8601)</td>
      <td>Ürünün oluşturulma tarihi</td>
      <td>2026-03-27T14:21:12.1607198</td>
    </tr>
    <tr>
      <td>updatedAt</td>
      <td>string / null</td>
      <td>Ürünün güncellenme tarihi (null ise güncellenmemiş)</td>
      <td>null</td>
    </tr>
    <tr>
      <td>categoryId</td>
      <td>UUID / string</td>
      <td>Ürünün ait olduğu kategori ID’si</td>
      <td>79b36246-3c43-40c1-8695-13425c171850</td>
    </tr>
    <tr>
      <td>title</td>
      <td>string</td>
      <td>Ürünün başlığı</td>
      <td>test</td>
    </tr>
    <tr>
      <td>description</td>
      <td>string</td>
      <td>Ürünün açıklaması</td>
      <td>test test test</td>
    </tr>
    <tr>
      <td>price.price</td>
      <td>decimal</td>
      <td>Ürünün fiyatı</td>
      <td>0.0100</td>
    </tr>
    <tr>
      <td>price.currency</td>
      <td>string</td>
      <td>Fiyatın para birimi</td>
      <td>TRY</td>
    </tr>
  </tbody>
</table>

##### Request Body Örneği

```json
 [
    {
        "id": "019d2fab-a860-774c-a3c0-9ff1bc7de95b",
        "createdAt": "2026-03-27T14:21:12.1607198",
        "updatedAt": null,
        "categoryId": "79b36246-3c43-40c1-8695-13425c171850",
        "title": "test",
        "description": "test test test",
        "price": {
            "price": 0.0100,
            "currency": "TRY"
        }
    }
]
```

### Logs

#### Search Products
##### HTTP Mehtod: GET
##### URL: /logs/query/search?key={key:string}&serviceName={service-name:string}&level={level:string}&traceId={traceId:string}&page={page:int}&pageSize={page-size:int}


| Parametre    | Açıklama                                     | Örnek Değer                                     |
|--------------|----------------------------------------------|-----------------------------------              |
| key          | Log sorgulama anahtarı                       | string                                          |
| serviceName  | Logların ait olduğu servis adı               | 'AuthService.Api', 'Gateway.Api', 'Product.Api' |
| level        | Log seviyesi                                 | 'Warning', 'Information', 'Error'               |
| traceId      | İlgili isteğe ait izleme (trace) ID’si       | string                                          |
| page         | Sayfa numarası (paginasyon)                  | number                                          |
| pageSize     | Her sayfadaki kayıt sayısı                   | number                                          |


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
      <td>429</td>
      <td>Rate limit exceeded</td>
    </tr>
    <tr>
      <td>500</td>
      <td>Sunucu hatası</td>
    </tr>
  </tbody>
</table>

##### Request Body

<p>
 Aşağıdaki tabloda verilen alanlara sahip obje listesi döner.
</p>

| Alan              | Açıklama                                                                                       | Örnek Değer                                                                 |
|------------------|------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------|
| `serviceName`     | Log kaydının ait olduğu servis adı                                                              | `"AuthService.Api"`                                                        |
| `timeStamp`       | Log kaydının oluştuğu tarih ve saat                                                            | `"2026-03-27T20:41:49.8636608+03:00"`                                     |
| `level`           | Log seviyesi (Information, Warning, Error, vb.)                                               | `"Information"`                                                            |
| `messageTemplate` | Log mesajının şablonu ve detayları                                                            | `"Request starting {Protocol} {Method} {Scheme}://{Host}{PathBase}{Path}{QueryString} - {ContentType} {ContentLength}"` |
| `traceId`         | İlgili isteğe ait izleme (trace) ID’si                                                        | `"3c120fd3df1cf63bf0fc3461f416851a"`                                      |
| `requestPaths`    | İstek yapılan endpoint’in path segmentleri                                                   | `["api", "users", "create"]`                                              |
| `exception`       | Eğer log bir hata içeriyorsa exception detayları, aksi takdirde `null`                        | `null`                                                                     |

##### Request Body Örneği
```json
{
        "serviceName": "AuthService.Api",
        "timeStamp": "2026-03-27T20:41:49.8636608+03:00",
        "level": "Information",
        "messageTemplate": "Request starting {Protocol} {Method} {Scheme}://{Host}{PathBase}{Path}{QueryString} - {ContentType} {ContentLength}",
        "traceId": "3c120fd3df1cf63bf0fc3461f416851a",
        "requestPaths": [
            "api",
            "users",
            "create"
        ],
        "exception": null
    },
```
