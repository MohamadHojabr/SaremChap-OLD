using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domainclasses.Modes;
using Domainclasses.Context;
using PagedList;
using System.IO;
using Domainclasses.Enums;
using SaremChap.Models;

namespace SaremChap.Areas.panel.Controllers
{



    public class GalleryItemController : Controller
    {

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
                string pathString = System.IO.Path.Combine(Directory, "gallery");
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


        private SaremContext db = new SaremContext();

        // GET: /panel/GalleryItem/
        public ActionResult Index(int? page)
        {
            var list = db.GalleryItems.Include(g => g.Gallery).OrderBy(x=>x.GalleryItemId);
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: /panel/GalleryItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryItem galleryitem = db.GalleryItems.Find(id);
            if (galleryitem == null)
            {
                return HttpNotFound();
            }
            return View(galleryitem);
        }

        // GET: /panel/GalleryItem/Create
        public ActionResult Create()
        {
            ViewBag.GalleryId = new SelectList(db.Galleries, "GalleryId", "name");
            return View();
        }

        // POST: /panel/GalleryItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="GalleryItemId,GalleryId,name,describtion,url")] GalleryItem galleryitem, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {

                    galleryitem.FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName);
                    galleryitem.FileType = FileType.Photo;
                   
                   
                    CreatePath();
                    var path = string.Format("{0}\\{1}", Path, galleryitem.FileName);
                    upload.SaveAs(path);
                }

                db.GalleryItems.Add(galleryitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GalleryId = new SelectList(db.Galleries, "GalleryId", "name", galleryitem.GalleryId);
            return View(galleryitem);
        }

        // GET: /panel/GalleryItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryItem galleryitem = db.GalleryItems.Find(id);
            if (galleryitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.GalleryId = new SelectList(db.Galleries, "GalleryId", "name", galleryitem.GalleryId);
            return View(galleryitem);
        }

        // POST: /panel/GalleryItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="GalleryItemId,GalleryId,name,describtion,url")] GalleryItem galleryitem, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var oldPath = string.Format("{0}\\{1}", Path, galleryitem.FileName);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                if (upload != null && upload.ContentLength > 0)
                {
                    galleryitem.FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName);
                    galleryitem.FileType = FileType.Photo;
                    CreatePath();
                    var path = string.Format("{0}\\{1}", Path, galleryitem.FileName);
                    upload.SaveAs(path);
                }


                db.Entry(galleryitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GalleryId = new SelectList(db.Galleries, "GalleryId", "name", galleryitem.GalleryId);
            return View(galleryitem);
        }

        // GET: /panel/GalleryItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryItem galleryitem = db.GalleryItems.Find(id);
            if (galleryitem == null)
            {
                return HttpNotFound();
            }
            return View(galleryitem);
        }

        // POST: /panel/GalleryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GalleryItem galleryitem = db.GalleryItems.Find(id);
            db.GalleryItems.Remove(galleryitem);
            var path = string.Format("{0}\\{1}", Path, galleryitem.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult imageList(int? imageid)
        //{
        //    if (imageid.HasValue)
        //    {

        //        var images = new ImagesModel();
        //        //Read out files from the files directory
        //        var files = Directory.GetFiles(Server.MapPath(@"~/Images/WallImages/imagepath"));
        //        //Add them to the model
        //        foreach (var file in files)
        //            images.Images.Add(Path.GetFileName(file));
        //        images.ImageId = imageid.Value;

        //        return PartialView("Partials/_imageList", images);

        //    }
        //    else
        //    {

        //        var images = new ImagesModel();
        //        //Read out files from the files directory
        //        var files = Directory.GetFiles(Server.MapPath(@"~/Images/WallImages/imagepath"));
        //        //Add them to the model
        //        foreach (var file in files)
        //            images.Images.Add(Path.GetFileName(file));


        //        return PartialView("Partials/_imageList", images);

        //    }
        //}

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
