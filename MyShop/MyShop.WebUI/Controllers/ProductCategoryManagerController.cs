﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext)
        {
            context = productCategoryContext;
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