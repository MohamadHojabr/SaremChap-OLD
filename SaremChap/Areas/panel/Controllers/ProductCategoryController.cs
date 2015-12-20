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
using Newtonsoft.Json;
using SaremChap.Models;
using PagedList;

namespace SaremChap.Areas.panel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
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
                string pathString = System.IO.Path.Combine(Directory, "productCategory");
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


        // GET: /panel/ProductCategory/
        public ActionResult Index(int? page)
        {
            var list = db.ProductCategories.OrderBy(x => x.ProductCategoryID);
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult jIndex()
        {
            var jlist = db.ProductCategories.ToList();
            var list = JsonConvert.SerializeObject(jlist, Formatting.None, new JsonSerializerSettings()
            {
    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore});


            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: /panel/ProductCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        // GET: /panel/ProductCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /panel/ProductCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCategoryID,name,describtion,Tags,SuperCategory")] ProductCategory productcategory ,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new ProductCategoryFiles
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        FileType = FileType.Photo
                    };
                    productcategory.ProductCategoryFileses = new List<ProductCategoryFiles>();
                    productcategory.ProductCategoryFileses.Add(photo);
                    CreatePath();
                    var path = string.Format("{0}\\{1}", Path, photo.FileName);
                    upload.SaveAs(path);
                }

                db.ProductCategories.Add(productcategory);
                db.SaveChanges();
                updateSiteMap updateSiteMap = new updateSiteMap();
                updateSiteMap.UpdateSiteMap("http://www.saremchap.ir/view/Product/Category/" + productcategory.ProductCategoryID, "add");

                return RedirectToAction("Index");
            }

            return View(productcategory);
        }

        // GET: /panel/ProductCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productcategory =
                db.ProductCategories.Find(id);

            
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        // POST: /panel/ProductCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductCategoryID,name,imege,Tags,describtion,SuperCategory")] ProductCategory productcategory,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var files =
                    db.ProductCategoryFiles.FirstOrDefault(p => p.ProductCategoryID == productcategory.ProductCategoryID);
                if (upload != null && upload.ContentLength > 0)
                {
                    if (files!=null)
                    {
                        var oldPath = string.Format("{0}\\{1}", Path, files.FileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }

                        db.ProductCategoryFiles.Remove(files);
                    }
                    var photo = new ProductCategoryFiles
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        FileType = FileType.Photo,
                        ProductCategoryID = productcategory.ProductCategoryID
                        
                    };
                    productcategory.ProductCategoryFileses = new List<ProductCategoryFiles>();
                    productcategory.ProductCategoryFileses.Add(photo);
                    CreatePath();
                    var path = string.Format("{0}\\{1}", Path, photo.FileName);
                    upload.SaveAs(path);
                    db.ProductCategoryFiles.Add(photo);

                }

                db.Entry(productcategory).State = EntityState.Modified;
                db.SaveChanges();
                updateSiteMap updateSiteMap = new updateSiteMap();
                updateSiteMap.UpdateSiteMap("http://www.saremchap.ir/view/Product/Category/" + productcategory.ProductCategoryID, "edit");
                return RedirectToAction("Index");
            }
            return View(productcategory);
        }

        // GET: /panel/ProductCategory/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        // POST: /panel/ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var files =
                db.ProductCategoryFiles.FirstOrDefault(p => p.ProductCategoryID == id);
            if (files!=null)
            {
                var oldPath = string.Format("{0}\\{1}", Path, files.FileName);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                db.ProductCategoryFiles.Remove(files);

            }
            ProductCategory productcategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productcategory);
            db.SaveChanges();
            updateSiteMap updateSiteMap = new updateSiteMap();
            updateSiteMap.UpdateSiteMap("http://www.saremchap.ir/view/Product/Category/" + productcategory.ProductCategoryID, "delete");
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
