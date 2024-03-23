using AutoMapper;
using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs;
using BASEAPP.Models.DTOs.Product;
using BASEAPP.Models.DTOs.User;
using BASEAPP.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.DataAccess.Repository
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext db, IMapper mapper) : base(db) 
        {
            _mapper = mapper;
        }

        public async Task<Product> CreateProductAsync(ProductCreateDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);

                var existingCategory = await _db.Categories.FindAsync(productDto.CategoryId);
                var existingBrand = await _db.Brands.FindAsync(productDto.BrandId);

                if (existingCategory != null)
                {
                    product.Category = existingCategory;
                }
                else
                {
                    var errorMessage = $"Category with ID {productDto.CategoryId} not found.";
                    throw new Exception(errorMessage);
                }

                if (existingBrand != null)
                {
                    product.Brand = existingBrand;
                }
                else
                {
                    var errorMessage = $"Brand with ID {productDto.BrandId} not found.";
                    throw new Exception(errorMessage);
                }

                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while creating the product.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<ProductDto>> GetByName(string name)
        {
            try
            {
                var products = await _db.Products
                    .Where(c => c.Name.Contains(name))
                    .ToListAsync();

                var productDtos = _mapper.Map<List<ProductDto>>(products);
                return productDtos;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching products by name.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsAsync(int page, int pageSize)
        {
            try
            {
                var products = await _db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalProducts = await _db.Products.CountAsync();

                var productDtos = _mapper.Map<List<ProductDto>>(products);

                var pagedResult = new PagedResultDto<ProductDto>
                {
                    TotalItems = totalProducts,
                    Items = productDtos
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching the list of products.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<bool> UpdateProductAsync(int productId, ProductUpdateDto productDto)
        {
            try
            {
                var product = await _db.Products.FindAsync(productId);

                if (product == null)
                {
                    return false;
                }

                _mapper.Map(productDto, product);
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while updating the product.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetTopRatedProductsAsync(int page, int pageSize)
        {
            try
            {
                var topRatedProductDtos = await _db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .OrderByDescending(p => p.AverageRating)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => _mapper.Map<ProductDto>(p))
                    .ToListAsync();

                var totalProducts = await _db.Products.CountAsync();

                var pagedResult = new PagedResultDto<ProductDto>
                {
                    TotalItems = totalProducts,
                    Items = topRatedProductDtos
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching top-rated products.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsByPriceDescendingAsync(int page, int pageSize)
        {
            try
            {
                var products = await _db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .OrderByDescending(p => p.Price)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalProducts = await _db.Products.CountAsync();

                var productDtos = _mapper.Map<List<ProductDto>>(products);

                var pagedResult = new PagedResultDto<ProductDto>
                {
                    TotalItems = totalProducts,
                    Items = productDtos
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching products by price (descending).";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsByPriceAscendingAsync(int page, int pageSize)
        {
            try
            {
                var products = await _db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .OrderBy(p => p.Price)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalProducts = await _db.Products.CountAsync();

                var productDtos = _mapper.Map<List<ProductDto>>(products);

                var pagedResult = new PagedResultDto<ProductDto>
                {
                    TotalItems = totalProducts,
                    Items = productDtos
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching products by price (ascending).";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsInPriceRangeAsync(double minPrice, double maxPrice, int page, int pageSize)
        {
            try
            {
                var products = await _db.Products
                    .OrderBy(p => p.Id)
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalProducts = await _db.Products
                    .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                    .CountAsync();

                var productDtos = _mapper.Map<List<ProductDto>>(products);

                var pagedResult = new PagedResultDto<ProductDto>
                {
                    TotalItems = totalProducts,
                    Items = productDtos
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching products in the price range.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsByCategoryAsync(string categoryName, int page, int pageSize)
        {
            try
            {
                var products = await _db.Products
                    .OrderBy(p => p.Id)
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Where(p => p.Category.Name.Equals(categoryName))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalProducts = await _db.Products
                    .Where(p => p.Category.Name.StartsWith(categoryName))
                    .CountAsync();

                var productDtos = _mapper.Map<List<ProductDto>>(products);

                var pagedResult = new PagedResultDto<ProductDto>
                {
                    TotalItems = totalProducts,
                    Items = productDtos
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching products by category.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsByBrandAsync(string brandName, int page, int pageSize)
        {
            try
            {
                var products = await _db.Products
                    .OrderBy(p => p.Id)
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Where(p => p.Brand.Name.Equals(brandName))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalProducts = await _db.Products
                    .Where(p => p.Brand.Name.Equals(brandName))
                    .CountAsync();

                var productDtos = _mapper.Map<List<ProductDto>>(products);

                var pagedResult = new PagedResultDto<ProductDto>
                {
                    TotalItems = totalProducts,
                    Items = productDtos
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching products by brand.";
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
