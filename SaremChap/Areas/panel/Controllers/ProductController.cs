using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domainclasses.Modes;
using Domainclasses.Context;
using Domainclasses.Enums;
using SaremChap.Models;
using PagedList;
using File = Domainclasses.Modes.ProductFiles;


namespace SaremChap.Areas.panel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private SaremContext db = new SaremContext();

        private string Directory
        {
            get
            {
                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Services", Server.MapPath(@"\"))).ToString();
                return originalDirectory;
            }
        }

        private string Path
        {
            get
            {
                string pathString = System.IO.Path.Combine(Directory, "service");
                return pathString;
            }
        }

        private bool PathIsExists
        {
            get
            {
                if (System.IO.Directory.Exists(Path)) return true;
                return false;
            }
        }

        private void CreatePath()
        {
            if (!PathIsExists)
            {
                System.IO.Directory.CreateDirectory(Path);
            }
            else
            {
                //todo
            }

        }


        // GET: /panel/Product/
        public ActionResult Index(int? page)
        {
            var list = db.Products.Include(p => p.ProductCategory).OrderBy(x=>x.ProductID);

            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: /panel/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /panel/Product/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "name");
            return View();
        }

        // POST: /panel/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductCategoryID,name,describtion")] Product product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new File
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        FileType = FileType.Photo
                    };
                    product.Files = new List<File>();
                    product.Files.Add(photo);
                    CreatePath();
                    var path = string.Format("{0}\\{1}", Path, photo.FileName);
                    upload.SaveAs(path);
                }
                
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "name", product.ProductCategoryID);
            return View(product);
        }

        // GET: /panel/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "name", product.ProductCategoryID);
            return View(product);
        }

        // POST: /panel/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductID,ProductCategoryID,name,imege,describtion")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "name", product.ProductCategoryID);
            return View(product);
        }

        // GET: /panel/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /panel/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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
