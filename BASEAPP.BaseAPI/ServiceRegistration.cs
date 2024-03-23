using AutoMapper;
using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository;
using BASEAPP.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.BaseAPI
{
    public static class ServiceRegistration
    {
        // Đăng ký các dịch vụ trong container Dependency Injection của ASP.NET Core.
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Cấu hình DbContext để kết nối đến cơ sở dữ liệu.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Cấu hình Mapper
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);

            // Đăng ký dịch vụ thao tác với các danh mục ở dưới trong cơ sở dữ liệu.
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        }
    }

}
