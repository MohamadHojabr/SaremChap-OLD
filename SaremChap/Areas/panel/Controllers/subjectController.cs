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

namespace SaremChap.Areas.panel.Controllers
{
    public class subjectController : Controller
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
                string pathString = System.IO.Path.Combine(Directory, "subject");
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


        // GET: /panel/subject/
        public ActionResult Index(int? page)
        {
            var list = db.Subjects.Include(s => s.Chapter).OrderBy(x=>x.SubjectId);
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: /panel/subject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: /panel/subject/Create
        public ActionResult Create()
        {
            ViewBag.ChapterId = new SelectList(db.Chapters, "ChapterId", "Name");
            return View();
        }

        // POST: /panel/subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectId,ChapterId,Authors,SubjectLead,SubjectContent,Status,SubjectDate")] Subject subject, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new SubjectFiles
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        FileType = FileType.Photo
                    };
                    subject.SubjectFiles = new List<SubjectFiles> {photo};
                    CreatePath();
                    var path = string.Format("{0}\\{1}", Path, photo.FileName);
                    upload.SaveAs(path);
                }


                db.Subjects.Add(subject);
                db.SaveChanges();
                updateSiteMap updateSiteMap = new updateSiteMap();
                updateSiteMap.UpdateSiteMap("http://www.saremchap.ir/view/Subject/post/" + subject.SubjectId, "add");

                return RedirectToAction("Index");
            }

            ViewBag.ChapterId = new SelectList(db.Chapters, "ChapterId", "Name", subject.ChapterId);
            return View(subject);
        }

        // GET: /panel/subject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChapterId = new SelectList(db.Chapters, "ChapterId", "Name", subject.ChapterId);
            return View(subject);
        }

        // POST: /panel/subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectId,ChapterId,Authors,SubjectLead,SubjectContent,Status,SubjectDate")] Subject subject, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                var file = db.SubjectFileses.FirstOrDefault(p => p.SubjectId == subject.SubjectId);
                if (file != null)
                {
                    db.SubjectFileses.Remove(file);
                    var oldPath = string.Format("{0}\\{1}", Path, file.FileName);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new SubjectFiles
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        FileType = FileType.Photo,
                        SubjectId = subject.SubjectId
                    };
                    subject.SubjectFiles = new List<SubjectFiles>();
                    subject.SubjectFiles.Add(photo);
                    CreatePath();
                    var path = string.Format("{0}\\{1}", Path, photo.FileName);
                    db.SubjectFileses.Add(photo);
                    upload.SaveAs(path);
                }

                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                updateSiteMap updateSiteMap = new updateSiteMap();
                updateSiteMap.UpdateSiteMap("http://www.saremchap.ir/view/Subject/post/" + subject.SubjectId, "edit");

                return RedirectToAction("Index");
            }
            ViewBag.ChapterId = new SelectList(db.Chapters, "ChapterId", "Name", subject.ChapterId);
            return View(subject);
        }

        // GET: /panel/subject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: /panel/subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var file = db.SubjectFileses.FirstOrDefault(p => p.SubjectId == id);
            if (file != null)
            {
                db.SubjectFileses.Remove(file);
                var oldPath = string.Format("{0}\\{1}", Path, file.FileName);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

            }


            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            updateSiteMap updateSiteMap = new updateSiteMap();
            updateSiteMap.UpdateSiteMap("http://www.saremchap.ir/view/Subject/post/" + subject.SubjectId, "delete");

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
