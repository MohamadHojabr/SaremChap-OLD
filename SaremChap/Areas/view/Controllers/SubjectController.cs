using Domainclasses.Context;
using Domainclasses.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaremChap.Areas.view.Controllers
{
    public class SubjectController : Controller
    {
        private SaremContext db = new SaremContext();
        //
        // GET: /view/Subject/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult list(int id)
        {
            var list = db.Subjects.Where(x => x.Status != Status.Disable)
                .Where(x=>x.ChapterId==id)
                .OrderByDescending(x => x.SubjectDate).ToList();
            return View(list);
        }

        public ActionResult post(int id)
        {
            var post = db.Subjects.Find(id);
            return View(post);
        }

	}
}