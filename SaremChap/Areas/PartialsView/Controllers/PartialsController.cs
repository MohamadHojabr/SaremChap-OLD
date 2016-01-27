using System.Linq;
using System.Web.Mvc;
using Domainclasses.Context;
using Domainclasses.Modes;

namespace SaremChap.Areas.PartialsView.Controllers
{
    public class PartialsController : Controller
    {
        private readonly SaremContext db = new SaremContext();
        //
        // GET: /PartialsView/Partials/
        [ChildActionOnly]
        public ActionResult indexSlideShow()
        {
            var subjects = db.Subjects.Where(x => x.Status == Status.Slideshow).ToList();
            return PartialView("indexSlideShow", subjects);
        }

        [ChildActionOnly]
        public ActionResult serviceSection()
        {
            return PartialView("serviceSection");
        }

        [ChildActionOnly]
        public ActionResult purchaseSection()
        {
            var subjects = db.Subjects.Where(x => x.Status == Status.Special).OrderByDescending(s=>s.SubjectDate).FirstOrDefault();
            return PartialView("purchaseSection", subjects);
        }

        [ChildActionOnly]
        public ActionResult ProductIntroductionSection()
        {
            return PartialView("ProductIntroductionSection");
        }

        [ChildActionOnly]
        public ActionResult ServicesSection()
        {
            return PartialView("ServicesSection", db.ProductCategories.ToList());
        }

        [ChildActionOnly]
        public ActionResult TestimonialsSection()
        {
            var list = db.Subjects.Where(x => x.Status != Status.Products)
                .OrderByDescending(x => x.SubjectDate)
                .Take(10);
            return PartialView("TestimonialsSection", list);
        }

        [ChildActionOnly]
        public ActionResult parallaxSection()
        {
            return PartialView("parallaxSection");
        }

        [ChildActionOnly]
        public ActionResult partnerSection()
        {
            return PartialView("partnerSection");
        }

        [ChildActionOnly]
        public ActionResult uploadPanel()
        {
            return PartialView("uploadPanel");
        }

        [ChildActionOnly]
        public ActionResult LSCategoryList()
        {
            var list = db.Chapters.ToList();
            return PartialView("LSCategoryList", list);
        }

        [ChildActionOnly]
        public ActionResult LatesPost()
        {
            var list = db.Subjects.Where(x => x.Status != Status.Disable)
                .OrderByDescending(x => x.SubjectDate)
                .Take(5);
            return PartialView("LatesPost", list);
        }

        [ChildActionOnly]
        public ActionResult ChapterInMenu()
        {
            var list = db.Chapters.ToList();
            return PartialView("ChapterInMenu", list);
        }

        [ChildActionOnly]
        public ActionResult ProductInMenu()
        {
            var list = db.ProductCategories.ToList();
            return PartialView("ProductInMenu", list);
        }

        [ChildActionOnly]
        public ActionResult GalleriesInMenu()
        {
            var list = db.Galleries.ToList();
            return PartialView("GalleriesInMenu", list);
        }
    }
}