﻿namespace Sales.Backend.Controllers
{
    using Sales.Backend.Helpers;
    using Sales.Backend.Models;
    using Sales.Common.Model;
    using System;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize(Roles = RolesHelper.PowerUser)]
    public class ProductsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductView view)
        {
            if (ModelState.IsValid)
            {
                var product = this.ToView(view);

                using (var transaction = db.Database.BeginTransaction())
                {

                    db.Products.Add(product);
                    try
                    {
                        await db.SaveChangesAsync();

                        view.ProductId = product.ProductId;

                        var pic = string.Empty;
                        var folder = "~/Content/Products";

                        if (view.LifeLogo != null)
                        {
                            pic = FilesHelper.UploadPhoto(view.LifeLogo, folder, string.Format("{0}", product.BarCode), string.Format("{0}", product.ProductId));

                            product.ImageMimeType = view.LifeLogo.ContentType;
                            int length = view.LifeLogo.ContentLength;
                            byte[] buffer = new byte[length];
                            view.LifeLogo.InputStream.Read(buffer, 0, length);
                            product.ImagenProduct = buffer;
                        }

                        if (!string.IsNullOrEmpty(pic))
                        {
                            product.ImagePath = pic;
                            db.Entry(product).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                        }


                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("_Index"))
                        {
                            ModelState.AddModelError(string.Empty, "There are a record with the same value");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.Message);

                            string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                            message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                            message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                            message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                            ModelState.AddModelError(string.Empty, message);
                        }
                    }
                }
            }

            return View(view);
        }

        private Product ToView(ProductView view)
        {
            return new Product
            {
                Description = view.Description,
                BarCode = view.BarCode,
                Price = view.Price,
                Remarks = view.Remarks,
                IsAvailable= view.IsAvailable,
                PublishOn = view.PublishOn,
            };
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var ViewProduct = this.ToProduct(product);

            return View(ViewProduct);
        }

        private ProductView ToProduct(Product product)
        {
            return new ProductView
            {
                BarCode = product.BarCode,
                Description = product.Description,
                ImagePath = product.ImagePath,
                ImageMimeType = product.ImageMimeType,
                ImagenProduct = product.ImagenProduct,
                Price = product.Price,
                Remarks = product.Remarks,
                ProductId = product.ProductId,
                IsAvailable = product.IsAvailable,
                PublishOn = product.PublishOn,
            };
        }
        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductView view)
        {
            if (ModelState.IsValid)
            {
                var product = this.ToProductView(view);

                var pic = view.ImagePath;
                var folder = "~/Content/Products";


                if (view.LifeLogo != null)
                {
                    pic = FilesHelper.UploadPhoto(view.LifeLogo, folder, string.Format("{0}", product.BarCode), string.Format("{0}", product.ProductId));

                    view.ImageMimeType = view.LifeLogo.ContentType;
                    int length = view.LifeLogo.ContentLength;
                    byte[] buffer = new byte[length];
                    view.LifeLogo.InputStream.Read(buffer, 0, length);
                    view.ImagenProduct = buffer;
                    product.ImagenProduct = buffer;
                }
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Entry(product).State = EntityState.Modified;
                    try
                    {
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("_Index"))
                        {
                            ModelState.AddModelError(string.Empty, "There are a record with the same value");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.Message);

                            string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                            message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                            message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                            message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                            ModelState.AddModelError(string.Empty, message);
                        }
                    }
                }
            }
            return View(view);
        }

        private Product ToProductView(ProductView view)
        {
            return new Product
            {
                BarCode = view.BarCode,
                Description = view.Description,
                ImagePath = view.ImagePath,
                ImageMimeType = view.ImageMimeType,
                ImagenProduct = view.ImagenProduct,
                Price = view.Price,
                Remarks = view.Remarks,
                ProductId = view.ProductId,
                IsAvailable = view.IsAvailable,
                PublishOn = view.PublishOn,
            };
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                db.Products.Remove(product);
                try
                {
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ModelState.AddModelError(string.Empty, "The record can't be delete because it has related records");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);

                        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                        message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                        message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                        message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                        ModelState.AddModelError(string.Empty, message);

                    }
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
