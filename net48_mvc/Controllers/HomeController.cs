using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net48_mvc.Entities; 

namespace net48_mvc.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<pegawai_tbl> _lstPegawai; 
            using (var r = new DBContextPegawai())
            {
                _lstPegawai = r.pegawai_tbl.ToList(); 
            }

            return View(_lstPegawai);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult create_get()
        {
            return View(); 
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult create_post()
        {
            var pegawai_models = new pegawai_tbl();
            TryUpdateModel(pegawai_models);
            pegawai_models.ID = Guid.NewGuid(); 
            using (var r = new DBContextPegawai())
            {
                r.pegawai_tbl.Add(pegawai_models);
                r.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Update")]
        public ActionResult update_get(Guid id)
        {
            var model = new pegawai_tbl();
            TryUpdateModel(model);

            using (var r = new DBContextPegawai())
            {
                model = r.pegawai_tbl.Where(x => x.ID == id).FirstOrDefault();
            }

            List<SelectListItem> lstHoby = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "1. Sepak Bola",
                    Value = "1"
                }, new SelectListItem
                {
                    Text = "2. Voli",
                    Value = "2"
                }, new SelectListItem
                {
                    Text = "3. Tenis Meja ",
                    Value = "3"
                }
            }; 

            return View(model);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult update_post(Guid id)
        {
            var menumodel = new pegawai_tbl();
            TryUpdateModel(menumodel);

            using (var r = new DBContextPegawai())
            {
                var m = r.pegawai_tbl.Where(x => x.ID == id).FirstOrDefault();
                TryUpdateModel(m);
                r.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Details")]
        public ActionResult get_detail(Guid id)
        {
            var tbl = new pegawai_tbl();
            TryUpdateModel(tbl);

            using (var r = new DBContextPegawai())
            {
                tbl = r.pegawai_tbl.FirstOrDefault(x => x.ID == id);
            }

            return View(tbl);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete_Get(Guid id)
        {
            var tbl = new pegawai_tbl();
            TryUpdateModel(tbl);

            using (var r = new DBContextPegawai())
            {
                tbl = r.pegawai_tbl.FirstOrDefault(x => x.ID == id);
            }

            return View(tbl);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(Guid id)
        {
            var tbl = new pegawai_tbl();
            TryUpdateModel(tbl);

            using (var r = new DBContextPegawai())
            {
                var m = r.pegawai_tbl.Remove(r.pegawai_tbl.FirstOrDefault(x => x.ID == id));
                TryUpdateModel(m);
                r.SaveChanges();
            }

            return RedirectToAction("Index");
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
    }
}