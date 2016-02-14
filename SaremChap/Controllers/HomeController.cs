using Domainclasses.Context;
using SaremChap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaremChap.Controllers
{
    public class HomeController : Controller
    {
        private SaremContext db = new SaremContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        public ActionResult imageGallery()
        {
            var GalleryList = db.Galleries.ToList();

            return View(GalleryList);
        }

        public ActionResult SelectGallery(int id)
        {
            var Gallery = db.Galleries.Where(x=>x.GalleryId==id).ToList();

            return View(Gallery);
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Accountnumber()
        {
           
            return View();
        }
        public ActionResult HowToOrder()
        {
           
            return View();
        }

        public ActionResult uploadManager()
        {
            return View();
        }

        public ActionResult imageList(int? imageid)
        {
            if (imageid.HasValue)
            {

                var images = new ImagesModel();
                //Read out files from the files directory
                var files = Directory.GetFiles(Server.MapPath(@"~/Images/WallImages/imagepath"));
                //Add them to the model
                foreach (var file in files)
                    images.Images.Add(Path.GetFileName(file));
                images.ImageId = imageid.Value;

                return PartialView("Partials/_imageList", images);

            }
            else
            {

                var images = new ImagesModel();
                //Read out files from the files directory
                var files = Directory.GetFiles(Server.MapPath(@"~/Images/WallImages/imagepath"));
                //Add them to the model
                foreach (var file in files)
                    images.Images.Add(Path.GetFileName(file));


                return PartialView("Partials/_imageList", images);

            }
        }


        public void DeletePhoto(string photoFileName)
        {

            var photoName = "";
            photoName = photoFileName;
            string fullPath = Request.MapPath("~/Images/WallImages/imagepath/" + photoName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

        }

        [Authorize(Roles="Admin")]
        public ActionResult Controlpanel()
        {
            return View();
        }

        public ActionResult SaveUploadedFile()
        {
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

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}