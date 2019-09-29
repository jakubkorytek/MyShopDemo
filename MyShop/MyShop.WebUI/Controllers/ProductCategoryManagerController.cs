using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> categories = context.Collection().ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            ProductCategory category = new ProductCategory();
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
                context.Insert(category);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory categoryToEdit = context.Find(Id);
            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToEdit);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory category, string Id)
        {
            ProductCategory categoryToEdit = context.Find(Id);
            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                categoryToEdit.Name = category.Name;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);
            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);
            if (categoryToDelete == null)
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