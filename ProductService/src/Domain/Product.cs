using Shared;

namespace Domain
{
    public class Product : Entity
    {
        public Guid CategoryId { get; private set; }
        public ProductTitle Title { get; private set; } = null!;
        public ProductDescription Description { get; private set; } = null!;
        public ProductPrice Price { get; private set; } = null!;

        //for ef core
        private Product() { }

        public Product(Guid categoryId, ProductTitle title, ProductDescription description, ProductPrice price) : base()
        {

            if (categoryId == Guid.Empty)
                throw new ArgumentException("CategoryId cannot be empty.", nameof(categoryId));

            ArgumentNullException.ThrowIfNull(title, nameof(title));
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            ArgumentNullException.ThrowIfNull(price, nameof(price));

            CategoryId = categoryId;
            Title = title;
            Description = description;
            Price = price;
        }

        public void Update(ProductTitle title, ProductDescription description, ProductPrice price)
        {
            ArgumentNullException.ThrowIfNull(title, nameof(title));
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            ArgumentNullException.ThrowIfNull(price, nameof(price));

            Title = title;
            Description = description;
            Price = price;

            Update();
        }
    }
}
