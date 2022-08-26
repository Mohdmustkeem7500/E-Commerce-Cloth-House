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
    public class CategoryController : Controller
    {
       
        CategoriesService categoriesService = new CategoriesService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _CategoryTable(string search)
        {
            CategorySearchViewModel categorySearchViewModel = new CategorySearchViewModel();

            categorySearchViewModel.Categories = categoriesService.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                categorySearchViewModel.SearchTerm = search;

                categorySearchViewModel.Categories = categorySearchViewModel.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView("_CategoryTable", categorySearchViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel newCategoryViewModel = new NewCategoryViewModel();

            //newCategoryViewModel.AvailableCategories = categoriesService.GetCategories();

            return PartialView(newCategoryViewModel);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel newCategoryViewModel)
        {
            var newCategory = new Category();
            newCategory.Name = newCategoryViewModel.Name;
            newCategory.Description = newCategoryViewModel.Description;
            newCategory.ImageURL = newCategoryViewModel.ImageURL;
            newCategory.IsFeatured = newCategoryViewModel.IsFeatured;
            //newProduct.CategoryID = newCategoryViewModel.CategoryID;
            //newCategory.Category = categoriesService.GetCategory(newCategoryViewModel.CategoryID);

            categoriesService.SaveCategory(newCategory);

            return RedirectToAction("_CategoryTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {

            EditCategoryViewModel editCategoryViewModel = new EditCategoryViewModel();

            var category = categoriesService.GetCategory(ID);

            editCategoryViewModel.ID = category.ID;
            editCategoryViewModel.Name = category.Name;
            editCategoryViewModel.Description = category.Description;
            editCategoryViewModel.ImageURL = category.ImageURL;
            editCategoryViewModel.IsFeatured = category.IsFeatured;

            return PartialView(editCategoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel editCategoryViewModel)
        {

            var existingCategory = categoriesService.GetCategory(editCategoryViewModel.ID);
            existingCategory.Name = editCategoryViewModel.Name;
            existingCategory.Description = editCategoryViewModel.Description;
            existingCategory.ImageURL = editCategoryViewModel.ImageURL;
            existingCategory.IsFeatured = editCategoryViewModel.IsFeatured;

            categoriesService.UpdateCategory(existingCategory);

            return RedirectToAction("_CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            categoriesService.DeleteCategory(ID);

            return RedirectToAction("_CategoryTable");
        }
    }
}