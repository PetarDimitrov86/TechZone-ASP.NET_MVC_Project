﻿namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
    using System.Collections.Generic;
    using Models.ViewModels.Products;

    [RoutePrefix("Products")]
    public class ProductsController : Controller
    {
        private ProductsService _service;

        public ProductsController()
        {
            this._service = new ProductsService();
        }

        [Route("All")]
        public ActionResult All()
        {
            IEnumerable<GeneralProductPageViewModel> productVms = this._service.GetAllProducts();
            return View(productVms);
        }

        [Route("Details/{id=1}")]
        public ActionResult Details(int id = 1)
        {
            if (!this._service.ProductExists(id))
            {
                return RedirectToAction("All");
            }

            ProductDetailsViewModel productDetailsVm = this._service.GetProductDetails(id);
            return this.View(productDetailsVm);
        }

        [Route("HardwareSpecs/{id}")]
        [ChildActionOnly]
        public ActionResult HardwareSpecs(int id)
        {
            Dictionary<string, string> specs = this._service.GetProductSpecs(id);
            return this.PartialView("_ProductSpecsPartial", specs);
        }
    }
}