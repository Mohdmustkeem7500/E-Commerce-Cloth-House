using ClothBazar.App.ViewModels;
using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.App.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();
        CategoriesService categoriesService = new CategoriesService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(string search)
        {
            ProductSearchViewModel productSearchViewModel = new ProductSearchViewModel();

            productSearchViewModel.Products = productsService.GetProducts();

            if (!string.IsNullOrEmpty(search))
            {
                productSearchViewModel.SearchTerm = search;

                productSearchViewModel.Products = productSearchViewModel.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView(productSearchViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel newProductViewModel = new NewProductViewModel();

            newProductViewModel.AvailableCategories = categoriesService.GetCategories();

            return PartialView(newProductViewModel);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel newProductViewModel)
        {
            //CategoriesService categoriesService = new CategoriesService();

            var newProduct = new Product();
            newProduct.Name = newProductViewModel.Name;
            newProduct.Description = newProductViewModel.Description;
            newProduct.Price = newProductViewModel.Price;
            //newProduct.CategoryID = newCategoryViewModel.CategoryID;
            newProduct.Category = categoriesService.GetCategory(newProductViewModel.CategoryID);

            productsService.SaveProduct(newProduct);

            return RedirectToAction("Products");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel editProductViewModel = new EditProductViewModel();

            var product = productsService.GetProduct(ID);

            editProductViewModel.ID = product.ID;
            editProductViewModel.Name = product.Name;
            editProductViewModel.Description = product.Description;
            editProductViewModel.Price = product.Price;
            editProductViewModel.CategoryID = product.Category != null ? product.Category.ID : 0;
            editProductViewModel.CategoryID = product.Category.ID;
            //editProductViewModel.ImageURL = product.ImageURL;

            editProductViewModel.AvailableCategories = categoriesService.GetCategories();

            return PartialView(editProductViewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel editProductViewModel)
        {
            var existingProduct = productsService.GetProduct(editProductViewModel.ID);
            existingProduct.Name = editProductViewModel.Name;
            existingProduct.Description = editProductViewModel.Description;
            existingProduct.Price = editProductViewModel.Price;
            existingProduct.Category = categoriesService.GetCategory(editProductViewModel.CategoryID);

            productsService.UpdateProduct(existingProduct);

            return RedirectToAction("Products");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productsService.DeleteProduct(ID);

            return RedirectToAction("Products");
        }
    }
}