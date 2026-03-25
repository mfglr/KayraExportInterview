using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace Domain.Test
{
    public class ProductUnitTests
    {
        private readonly Guid _userId = Guid.NewGuid();
        private readonly Guid _categoryId = Guid.NewGuid();
        private readonly ProductTitle _title = new ("Valid Title");
        private readonly ProductDescription _description = new("Valid Description");
        private readonly ProductPrice _price = new (10.5m, Currency.USD.Clone());
        private readonly Product _product;
        
        public ProductUnitTests()
        {
            _product = new(_userId, _categoryId, _title, _description, _price);
        }

        [Fact]
        public void Product_ShouldCreateProduct_WhenValuesAreValid()
        {
            Assert.Equal(_product.CategoryId, _categoryId);
            Assert.Equal(_product.Title, _title);
            Assert.Equal(_product.Description, _description);
            Assert.Equal(_product.Price, _price);
        }
        [Fact]
        public void Product_ShouldProductThrowException_WhenCategoryIdIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new Product(_userId, default, _title, _description, _price));
        }
        [Fact]
        public void Product_ShouldProductThrowException_WhenTitleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(_userId, _categoryId, null!, _description, _price));
        }
        [Fact]
        public void Product_ShouldProductThrowException_WhenDescriptionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(_userId, _categoryId, _title, null!, _price));
        }
        [Fact]
        public void Product_ShouldProductThrowException_WhenPriceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(_userId, _categoryId, _title, _description, null!));
        }

        [Fact]
        public void Update_ShouldTrue_WhenUpdateSuccess()
        {
            var title = new ProductTitle("012");
            var description = new ProductDescription("0123456789");
            var price = new ProductPrice(15, Currency.TRY.Clone());
            var dateTimeBeforeUpdate = DateTime.UtcNow;
            
            _product.Update(title, description, price);

            Assert.Equal(title, _product.Title);
            Assert.Equal(description, _product.Description);
            Assert.Equal(price, _product.Price);
            Assert.NotNull(_product.UpdatedAt);
            Assert.True(_product.UpdatedAt >= dateTimeBeforeUpdate);
            Assert.True(_product.UpdatedAt <= DateTime.UtcNow);
        }
        [Fact]
        public void Update_ShouldProductThrowException_WhenTitleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _product.Update(null!, _description, _price));
        }
        [Fact]
        public void Update_ShouldProductThrowException_WhenDescriptionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _product.Update(_title, null!, _price));
        }
        [Fact]
        public void Update_ShouldProductThrowException_WhenPriceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _product.Update(_title, _description, null!));
        }
    }
}
