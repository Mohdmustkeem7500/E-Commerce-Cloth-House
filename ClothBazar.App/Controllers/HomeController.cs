﻿using ClothBazar.App.ViewModels;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.App.Controllers
{
    public class HomeController : Controller
    {
        CategoriesService categoriesService = new CategoriesService();
        public ActionResult Index()
        {
            HomeViewModels viewModel = new HomeViewModels();

            viewModel.FeaturedCategories = categoriesService.GetFeaturedCategories();
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}