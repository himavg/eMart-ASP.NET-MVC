using eMart.Data.Base;
using eMart.Data.ViewModels;
using eMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<NewProductDropdownsVM> GetNewProductDropdownsValues();

        Task AddNewProductAsync(NewProductVM data);

        Task UpdateProductAsync(NewProductVM data);
    }
}
