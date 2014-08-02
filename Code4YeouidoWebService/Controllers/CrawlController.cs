using System;
using Code4YeouidoWebService.Helpers;
using Code4YeouidoWebService.Models;
using System.Web.Mvc;

namespace Code4YeouidoWebService.Controllers
{
    public class CrawlController : Controller
    {
        private Code4YeouidoEntities db = new Code4YeouidoEntities();

        // GET: Crawl
        public ActionResult Index(int id = 1)
        {
            String msg = "CrawlBills - Ok";
            try
            {
                CrawlHelper.CrawlBills(db, id);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return Content(msg);
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