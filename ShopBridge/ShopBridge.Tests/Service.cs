using ShopBridge.Core.Domain.Catalog;
using ShopBridge.Core.Domain.Messages;
using ShopBridge.Core.Domain.Sale;
using ShopBridge.Core.Domain.Statistics;
using ShopBridge.Core.Domain.User;
using ShopBridge.Infrastructure;
using ShopBridge.Infrastructure.EFRepository;
using ShopBridge.Infrastructure.Services.Catalog;
using ShopBridge.Infrastructure.Services.Messages;
using ShopBridge.Infrastructure.Services.Sale;
using ShopBridge.Infrastructure.Services.Statistics;
using ShopBridge.Infrastructure.Services.User;

namespace ShopBridge.xUnitTest
{
    public class Service
    {
        public Service(ApplicationDbContext context)
        {
            // repository
            BillingAddressRepository = new Repository<BillingAddress>(context);
            CategoryRepository = new Repository<Category>(context);
            ImageRepository = new Repository<Image>(context);
            ManufacturerRepository = new Repository<Manufacturer>(context);
            OrderRepository = new Repository<Order>(context);
            OrderItemRepository = new Repository<OrderItem>(context);
            ProductRepository = new Repository<Product>(context);
            ProductCategoryMapping = new Repository<ProductCategoryMapping>(context);
            ProductImageMapping = new Repository<ProductImageMapping>(context);
            ProductManufacturerMapping = new Repository<ProductManufacturerMapping>(context);
            ProductSpecificationMapping = new Repository<ProductSpecificationMapping>(context);
            ReviewRepository = new Repository<Review>(context);
            SpecificationRepository = new Repository<Specification>(context);
            VisitorCountRepository = new Repository<VisitorCount>(context);
            OrderCountRepository = new Repository<OrderCount>(context);
            ContactUsMessageRepository = new Repository<ContactUsMessage>(context);

            // service
            BillingAddressService = new BillingAddressService(context, BillingAddressRepository);
            CategoryService = new CategoryService(context, CategoryRepository, ProductCategoryMapping);
            ManufacturerService = new ManufacturerService(context, ManufacturerRepository, ProductManufacturerMapping);
            OrderService = new OrderService(context, OrderCountService, OrderRepository, OrderItemRepository);
            ImageManagerService = new ImageManagerService(ImageRepository, ProductImageMapping);
            ProductService = new ProductService(context, ProductRepository);
            ReviewService = new ReviewService(context, ReviewRepository);
            SpecificationService = new SpecificationService(context, SpecificationRepository, ProductSpecificationMapping);
            VisitorCountService = new VisitorCountService(context, VisitorCountRepository);
            OrderCountService = new OrderCountService(OrderCountRepository);
            ContactUsService = new ContactUsService(ContactUsMessageRepository);
        }

        // repository
        public Repository<BillingAddress> BillingAddressRepository { get; set; }
        public Repository<Category> CategoryRepository { get; set; }
        public Repository<Image> ImageRepository { get; set; }
        public Repository<Manufacturer> ManufacturerRepository { get; set; }
        public Repository<Order> OrderRepository { get; private set; }
        public Repository<OrderItem> OrderItemRepository { get; private set; }
        public Repository<Product> ProductRepository { get; set; }
        public Repository<ProductCategoryMapping> ProductCategoryMapping { get; set; }
        public Repository<ProductImageMapping> ProductImageMapping { get; set; }
        public Repository<ProductManufacturerMapping> ProductManufacturerMapping { get; set; }
        public Repository<ProductSpecificationMapping> ProductSpecificationMapping { get; set; }
        public Repository<Review> ReviewRepository { get; set; }
        public Repository<Specification> SpecificationRepository { get; set; }
        public Repository<VisitorCount> VisitorCountRepository { get; set; }
        public Repository<OrderCount> OrderCountRepository { get; set; }
        public Repository<ContactUsMessage> ContactUsMessageRepository { get; set; }

        // service
        public BillingAddressService BillingAddressService { get; set; }
        public CategoryService CategoryService { get; set; }
        public ImageManagerService ImageManagerService { get; set; }
        public ManufacturerService ManufacturerService { get; set; }
        public OrderService OrderService { get; private set; }
        public ProductService ProductService { get; set; }
        public ReviewService ReviewService { get; set; }
        public SpecificationService SpecificationService { get; set; }
        public VisitorCountService VisitorCountService { get; set; }
        public OrderCountService OrderCountService { get; set; }
        public ContactUsService ContactUsService { get; set; }
    }
}
