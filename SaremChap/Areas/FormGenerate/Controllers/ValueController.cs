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

namespace SaremChap.Areas.FormGenerate.Controllers
{
    public class ValueController : Controller
    {
        private SaremContext db = new SaremContext();

        // GET: /FormGenerate/Value/
        public ActionResult Index()
        {
            var values = db.Values.Include(v => v.Field).Include(v => v.Form);
            return View(values.ToList());
        }

        // GET: /FormGenerate/Value/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // GET: /FormGenerate/Value/Create
        public ActionResult Create()
        {
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "TitleEn");
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Title");
            return View();
        }

        // POST: /FormGenerate/Value/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Val,FieldId,FormId")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Values.Add(value);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldId = new SelectList(db.Fields, "Id", "TitleEn", value.FieldId);
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Title", value.FormId);
            return View(value);
        }

        // GET: /FormGenerate/Value/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "TitleEn", value.FieldId);
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Title", value.FormId);
            return View(value);
        }

        // POST: /FormGenerate/Value/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Val,FieldId,FormId")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "TitleEn", value.FieldId);
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Title", value.FormId);
            return View(value);
        }

        // GET: /FormGenerate/Value/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // POST: /FormGenerate/Value/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Value value = db.Values.Find(id);
            db.Values.Remove(value);
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
