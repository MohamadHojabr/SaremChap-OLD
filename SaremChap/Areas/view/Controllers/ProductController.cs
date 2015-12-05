using Domainclasses.Context;
using Domainclasses.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaremChap.Areas.view.Controllers
{
    public class ProductController : Controller
    {
        private SaremContext db = new SaremContext();
        //
        // GET: /view/Product/
        public ActionResult Category(int id)
        {
            var category = db.ProductCategories.FirstOrDefault(p => p.ProductCategoryID == id);
            ViewBag.formid = db.ProductCategories.Where(m => m.ProductCategoryID == id).Select(x => x.Product);
            ViewBag.datenow = DateTime.Now.ToString("yyyy-MM-dd");
            return View(category);
        }
	}
}