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


namespace SaremChap.Areas.PartialsView.Controllers
{
    public class PartialsController : Controller
    {
        private SaremContext db = new SaremContext();
        //
        // GET: /PartialsView/Partials/
        public ActionResult indexSlideShow()
        {
            var subjects = db.Subjects.Where(x => x.Status == Status.Slideshow).ToList();
            return View(subjects);
        }

        public ActionResult  serviceSection()
        {
            return View();
        }
        public ActionResult purchaseSection()
        {
            var subjects = db.Subjects.Where(x => x.Status == Status.Special).FirstOrDefault();
            return View(subjects);

            
        }
        public ActionResult ProductIntroductionSection()
        {
            return View();
        }
        public ActionResult ServicesSection()
        {
           
            return View( db.ProductCategories.ToList());
        }
        public ActionResult TestimonialsSection()
        {
            var list = db.Subjects.Where(x => x.Status != Status.Products)
                .OrderByDescending(x => x.SubjectDate)
                .Take(10);
            return View(list);
        }
        public ActionResult parallaxSection()
        {
            return View();
        }
        public ActionResult partnerSection()
        {
            return View();
        }

        public ActionResult uploadPanel()
        {
            return View();
        }

        public ActionResult LSCategoryList()
        {
            var list = db.Chapters.ToList();
            return View(list);
        }

        public ActionResult LatesPost()
        {
            var list = db.Subjects.Where(x=>x.Status!=Status.Disable)
                .OrderByDescending(x=>x.SubjectDate)
                .Take(5);
            return View(list);
        }

        public ActionResult ChapterInMenu()
        {
            var list = db.Chapters.ToList();
            return View(list);
        }

        public ActionResult ProductInMenu()
        {
            var list = db.ProductCategories.ToList();
            return View(list);
        }



	}
}