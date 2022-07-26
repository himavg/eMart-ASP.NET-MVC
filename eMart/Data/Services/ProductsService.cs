using eMart.Data.Base;
using eMart.Data.ViewModels;
using eMart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Data.Services
{
    public class ProductsService:EntityBaseRepository<Product>,IProductsService
    {

        private readonly AppDbContext _context;

        public ProductsService(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                ProductCategory = data.ProductCategory,
                ProductOwnerId = data.OwnerId
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Owners = await _context.Owners.OrderBy(n => n.Name).ToListAsync()
            };
           

            return response;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .Include(o => o.ProductOwner)
                .FirstOrDefaultAsync(n => n.Id == id);
            return productDetails;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.StartDate = data.StartDate;
                dbProduct.EndDate = data.EndDate;
                dbProduct.ProductCategory = data.ProductCategory;
                dbProduct.ProductOwnerId = data.OwnerId;
                await _context.SaveChangesAsync();
            }

            
        }
    }
}
