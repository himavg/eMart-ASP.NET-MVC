using eMart.Data;
using eMart.Data.Services;
using eMart.Data.Static;
using eMart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync();
            return View(allProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allProducts.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
               
                var filteredResultNew = allProducts.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) ||
                    string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
                
                return View("Index", filteredResultNew);
            }

            return View("Index", allProducts);
        }

        //Get: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetProductByIdAsync(id);
            return View(movieDetail);
        }

        //Get: Products/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Owners = new SelectList(productDropdownsData.Owners, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //Get: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                StartDate = productDetails.StartDate,
                EndDate = productDetails.EndDate,
                Price = productDetails.Price,
                ImageURL = productDetails.ImageURL,
                ProductCategory = productDetails.ProductCategory,
                OwnerId = productDetails.ProductOwnerId,
            };

            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            ViewBag.Owners = new SelectList(productDropdownsData.Owners, "Id", "Name");

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
