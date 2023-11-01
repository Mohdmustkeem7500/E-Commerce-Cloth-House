using ClothBazar.App.ViewModels;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.App.Controllers
{
    public class ShopController : Controller 
    {

        //ProductsService productsService = new ProductsService();

        // GET: Shop
        public ActionResult Checkout()
        {
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;

                //var ids = productIDs.Split('-');

                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                checkoutViewModel.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                checkoutViewModel.CartProducts = ProductsService.Instance.GetProducts(checkoutViewModel.CartProductIDs);
            }

            return View(checkoutViewModel);
        }
    }
}