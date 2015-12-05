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
    public class FieldController : Controller
    {
        private SaremContext db = new SaremContext();

        // GET: /FormGenerate/Field/
        public ActionResult Index()
        {
            var fields = db.Fields.Include(f => f.Form);
            return View(fields.ToList());
        }

        public ActionResult Fieldlist(int id)
        {
            var fields = db.Fields.Where(x=>x.FormId==id).Include(f => f.Form);
            return View(fields.ToList());
        }


        // GET: /FormGenerate/Field/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Field field = db.Fields.Find(id);
            if (field == null)
            {
                return HttpNotFound();
            }
            return View(field);
        }

        // GET: /FormGenerate/Field/Create
        public ActionResult Create()
        {
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Fa_Title");
            return View();
        }

        // POST: /FormGenerate/Field/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TitleEn,TitleFa,RequiredField,DisabledField,Options,FieldType,FormId,Finance,EffectivePrice")] Field field)
        {
            var id = field.FormId;
            if (ModelState.IsValid)
            {
                db.Fields.Add(field);
                db.SaveChanges();
                return RedirectToAction("Fieldlist/" + id);
            }

            ViewBag.FormId = new SelectList(db.Forms, "Id", "Fa_Title", field.FormId);
            return View(field);
        }

        // GET: /FormGenerate/Field/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Field field = db.Fields.Find(id);
            if (field == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Fa_Title", field.FormId);
            return View(field);
        }

        // POST: /FormGenerate/Field/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TitleEn,TitleFa,RequiredField,DisabledField,Options,FieldType,FormId,Finance,EffectivePrice")] Field field)
        {
            if (ModelState.IsValid)
            {
                var id = field.FormId;
                db.Entry(field).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Fieldlist/"+id);
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Fa_Title", field.FormId);
            return View(field);
        }

        // GET: /FormGenerate/Field/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Field field = db.Fields.Find(id);
            if (field == null)
            {
                return HttpNotFound();
            }
            return View(field);
        }

        // POST: /FormGenerate/Field/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Field field = db.Fields.Find(id);
            db.Fields.Remove(field);
            db.SaveChanges();
            return RedirectToAction("Fieldlist/" + id);
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
