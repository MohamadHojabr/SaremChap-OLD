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
using SaremChap.Models;
using PagedList;

namespace SaremChap.Areas.panel.Controllers
{
    public class subjectController : Controller
    {
        private SaremContext db = new SaremContext();

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
        public ActionResult Create([Bind(Include="SubjectId,ChapterId,Authors,SubjectLead,SubjectContent,SubjectImage,Status,SubjectDate")] Subject subject)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include="SubjectId,ChapterId,Authors,SubjectLead,SubjectContent,SubjectImage,Status,SubjectDate")] Subject subject)
        {
            if (ModelState.IsValid)
            {
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
