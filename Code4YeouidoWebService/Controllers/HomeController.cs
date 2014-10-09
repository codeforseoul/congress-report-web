using Code4YeouidoWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Code4YeouidoWebService.Controllers
{
    public class HomeController : Controller
    {
        private Code4YeouidoEntities db = new Code4YeouidoEntities();
        public ActionResult Index()
        {
            //SELECT CityCompare FROM DistrictInfo GROUP BY CityCompare
            var cities = db.DistrictInfo.Select(e => new { e.CityCompare }).Distinct();

            List<String> listCC = new List<string>();
            foreach (var i in cities)
                listCC.Add(i.CityCompare);

            ViewBag.listCC = listCC;

            return View();
        }

        public ActionResult DistrictInfo(String city, String dist, String doffi, String proc = "city")
        {
            StringBuilder s = new StringBuilder();
            if (proc.Equals("city"))
            {
                //SELECT Dist FROM DistrictInfo WHERE CityCompare = '".$city."' GROUP BY Dist
                var list = db.DistrictInfo.Where(e => e.CityCompare.Equals(city)).Select(e => new { e.Dist }).Distinct();
                s.Append("<option value=\"none\">시/군/구</option>");
                foreach (var i in list)
                {
                    s.Append("<option value=\"" + i.Dist + "\">" + i.Dist + "</option>");
                }

            }
            else if (proc.Equals("dist"))
            {
                //SELECT Towns, DistOfficial FROM DistrictInfo WHERE CityCompare = '".$city."' AND Dist = '".$dist."'
                var list = db.DistrictInfo.Where(e => e.CityCompare.Equals(city) && e.Dist.Equals(dist)).Select(e => new { e.Towns, e.DistOfficial }).Distinct();
                s.Append("<option value=\"none\">읍/면/동</option>");
                foreach (var i in list.Select(e => new { e.DistOfficial }))
                {
                    foreach (var j in list.Where(e => e.DistOfficial.Equals(i.DistOfficial)))
                        s.Append("<option value=\"" + i.DistOfficial + "\">" + j.Towns + "</option>");
                }
            }
            else if (proc.Equals("person"))
            {
                //select * from MP where DistrictCompare = '".$city.$doffi."'
                var list = db.MP.Where(e => e.DistrictCompare.Equals(city + doffi));
                foreach (var i in list)
                {
                    s.Append(i.NameKr + "||+=+||" + city + " " + doffi + "||+=+||" + i.Party + "||+=+||" + i.Photo);
                }
            }

            return Content(s.ToString());
        }

        public ActionResult ApplyMailing(String email, String name, String doffi, String city)
        {
            /*$qry = sqlsrv_query($connect,"SELECT COUNT(Email) cn FROM MailList WHERE Email = '".$email."'");
            $row = sqlsrv_fetch_array($qry);
            if($row['cn'] > 0){
	            sqlsrv_query($connect,"UPDATE MailList SET Name = '".$name."', District = '".$doffi."', CityCompare = '".$city."', DistrictCompare = '".$city.$doffi."', WhenEditted = '".date('Y-m-d')."' WHERE Email = '".$email."'");
	            echo 'ok';
            } else {
	            sqlsrv_query($connect,"INSERT INTO MailList (Name, Email, District, CityCompare, DistrictCompare) VALUES ('".$name."', '".$email."', '".$doffi."', '".$city."', '".$city.$doffi."')");
	            echo 'ok';
            }?>*/
            String s;
            email = email.Trim();
            MailList m = db.MailList.FirstOrDefault(e => e.Email.Equals(email));

            if (m == null)
            {
                m = new MailList();
                m.Name = name;
                m.Email = email;
                m.District = doffi;
                m.CityCompare = city;
                m.DistrictCompare = city + doffi;
                m.WhenCreated = DateTime.Now;
                db.MailList.Add(m);
            }
            else
            {
                m.Name = name;
                m.District = doffi;
                m.CityCompare = city;
                m.DistrictCompare = city + doffi;
                m.WhenEditted = DateTime.Now;
            }
            s = "ok";

            db.SaveChanges();

            return Content(s);
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