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
    public class chapterController : Controller
    {
        private SaremContext db = new SaremContext();

        // GET: /panel/chapter/
        public ActionResult Index(int? page)
        {
            var list = db.Chapters.OrderBy(x => x.ChapterId);
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: /panel/chapter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // GET: /panel/chapter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /panel/chapter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ChapterId,Name")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Chapters.Add(chapter);
                db.SaveChanges();
                updateSiteMap updateSiteMap = new updateSiteMap();
                updateSiteMap.UpdateSiteMap("http://saremchap.ir/view/Subject/list/" + chapter.ChapterId, "add");
                return RedirectToAction("Index");
            }

            return View(chapter);
        }

        // GET: /panel/chapter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: /panel/chapter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ChapterId,Name")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                updateSiteMap updateSiteMap = new updateSiteMap();
                updateSiteMap.UpdateSiteMap("http://saremchap.ir/view/Subject/list/" + chapter.ChapterId, "edit");
                return RedirectToAction("Index");
            }
            return View(chapter);
        }

        // GET: /panel/chapter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: /panel/chapter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chapter chapter = db.Chapters.Find(id);
            db.Chapters.Remove(chapter);
            db.SaveChanges();
            updateSiteMap updateSiteMap = new updateSiteMap();
            updateSiteMap.UpdateSiteMap("http://saremchap.ir/view/Subject/list/" + chapter.ChapterId, "delete");
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
