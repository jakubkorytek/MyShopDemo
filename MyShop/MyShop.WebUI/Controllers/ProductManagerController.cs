using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategories;

        public ProductManagerController()
        {
            context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.productCategory = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductManagerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                context.Insert(viewModel.product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product productToEdit = context.Find(Id);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = new Product();
                viewModel.productCategory = productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel viewModel, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                productToEdit.Category = viewModel.product.Category;
                productToEdit.Description = viewModel.product.Description;
                productToEdit.Image = viewModel.product.Image;
                productToEdit.Name = viewModel.product.Name;
                productToEdit.Price = viewModel.product.Price;

                context.Commit();
                
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}