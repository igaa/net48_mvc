using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net48_mvc.Models; 

namespace net48_mvc.Controllers
{
    public class UploadFileController : Controller
    {
        private Guid p_id;
        private Guid photo; 

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {
               
                        string path = Path.Combine(Server.MapPath("~/Uploaded"),id.ToString()+".jpg");

                        var filesize = new System.IO.FileInfo(path).Length;

                        var Tokb = filesize / 1024; 

                        if (Tokb <=  1024)
                        {
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }

                            file.SaveAs(path);
                        }
                        else
                        {
                            ViewBag.FileStatus = "File can't more than 1 MB.";
                        }
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
            }
            return RedirectToAction("Details", "Home", new { id = id}); 

        }

        [HttpGet]
        [ActionName("Upload")]
        public ActionResult Upload_Get(Guid id)
        {
            p_id = id;
            UploadFileModels model = new UploadFileModels();
            model.id = id; 
            return View(model); 
        }
        // GET: upload
        public ActionResult Index()
        {
            return View("Upload");
        }
    }
}