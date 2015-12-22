using Domainclasses.Context;
using Domainclasses.Modes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SaremChap.Areas.FormGenerate.Controllers
{
    public class ShowFormController : Controller
    {
        private SaremContext db = new SaremContext();
        //
        // GET: /FormGenerate/ShowForm/
        public ActionResult ShowForm(int id)
        {
            var formid = db.Forms.Where(x => x.Product_ID == id).Select(x => x.Id).Single();

            var field = db.Fields.Where(x => x.FormId == formid);
            
            ViewBag.FormId = id;
            var pricelist = db.Forms.Find(formid).Product.Price;
            ViewBag.PriceList = pricelist;
            var relatedProduct = db.Forms.Find(formid).Product;
            ViewBag.RelatedProduct = relatedProduct;
            var form = db.Forms.Find(formid);
            ViewBag.form = form;

            return View(field);
        }




        [HttpPost]
        public ActionResult ShowForm(IEnumerable<Field> values)
        {
            Value vale = new Value();
 var date = DateTime.Now;
            var Sid = vale.FormId + vale.FieldId + vale.Id + date.Second + date.Millisecond * date.Minute;
           

            if (ModelState.IsValid)
            {
                foreach (var value in values)
                {
                  vale =  new Value { Val = value.TitleEn, FormId = value.FormId, FieldId = value.Id , Date = date , SubmitId=Sid} ;
                  db.Values.Add(vale);
                  db.SaveChanges();
                }


            }
            return RedirectToAction("SubmitOrder",new{id=Sid});
        }

        public ActionResult showFormResualtList(int id)
        {

            var list = db.Values.Where(x => x.FormId == id);
            ViewBag.FormInfo = db.Forms.Where(x => x.Id == id);
            return View(list.ToList());
        }

        public ActionResult showFormResualItem(int id)
        {

            var list = db.Values.Where(x => x.SubmitId == id);

            return View(list.ToList());
        }

        public ActionResult SubmitOrder(int id)
        {
            ViewBag.FormSubmitId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitOrder([Bind(Include = "OrderID,OrderDate,FirstName,LastName,Address,PostalCode,Phone,Mobile,Email,Total,SubmitFormId")] Order order)
        {
            var sid = order.SubmitFormId;
            if (ModelState.IsValid)
            {
                
               var list = db.Values.Where(x => x.SubmitId == sid);
               List<Value> Value_list = new List<Value>(list);
               order.Value = Value_list;
               order.OrderDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                var SubmitFormId = order.SubmitFormId;
                return RedirectToAction("GetTrackingnumber", new { Trackingnumber = SubmitFormId });
            }

            return View(order);
        }

        public ActionResult GetTrackingnumber(int Trackingnumber)
        {
            ViewBag.trackingnumber = Trackingnumber;
            return View();
        }

        public ActionResult OrderList(int? page, string sortOrder, string currentFilter, string searchString,string submitid)
        {
            var list = from s in db.Orders
                           select s;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (submitid != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    list = list.OrderBy(s => s.OrderDate);
                    break;
                case "date_desc":
                    list = list.OrderByDescending(s => s.SubmitFormId);
                    break;
                default:  // Name ascending 
                    list = list.OrderBy(s => s.LastName);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(submitid))
            {
                int numVal = Int32.Parse(submitid);
                list = list.Where(s => s.SubmitFormId == numVal);
                                       
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Invoice(int id)
        {
            var invoice = db.Orders.Where(x => x.SubmitFormId == id);
            var sid = id;
            ViewBag.field = db.Values.Where(x => x.SubmitId == sid);
            return View(invoice);
        }

        public ActionResult PrintInvoice(int id)
        {
            var invoice = db.Orders.Where(x => x.SubmitFormId == id);
            var sid = id;
            ViewBag.field = db.Values.Where(x => x.SubmitId == sid);
            return View(invoice);
        }



        public ActionResult SaveUploadedImage()
        {
            string fileName1 = null;
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "OrderImage");

                        fileName1 = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                       
                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, fileName1);
                        file.SaveAs(path);
                        ViewBag.filename = fileName1;
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fileName1 });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        public ActionResult DeleteUploadedImage(string fileName)
        {
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages\\OrderImage", Server.MapPath(@"\")));

            var oldPath = string.Format("{0}\\{1}", originalDirectory, fileName);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            return Json(new { Message = "حذف با موفقیت انجام شد" });
        }

	}
}