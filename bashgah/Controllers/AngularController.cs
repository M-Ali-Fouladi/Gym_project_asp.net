using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bashgah.Models;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Quartz;
using System.Data.SqlClient;
using WhatsAppApi;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Globalization;
using FarsiLibrary.Utils;
namespace bashgah.Controllers
{
    public class AngularController : Controller
    {
        private static HttpClient client = new HttpClient();
        private static string persiandate ;
        private bcontext5 db = new bcontext5();
        // GET: Angular
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            PersianDateTime p = new PersianDateTime(DateTime.Now);
            persiandate = (p.Year + "/" + p.Month + "/" + p.Day).ToString();
            ViewBag.persian = persiandate;
            return View();
        }

        public async Task<ActionResult> users_Read([DataSourceRequest] DataSourceRequest request)
        {
            var response = await client.GetAsync("http://www.iranaspweb.ir/api/usersController");
            var json = response.Content.ReadAsAsync<IEnumerable<user>>().Result.ToList();//serialize
        
            //TodoItem item = JsonConvert.DeserializeObject<TodoItem>(json);//deserilize
            return Json(json,JsonRequestBehavior.AllowGet);
        }

        public async Task<string> users_create(user model)
        {
            //model.birthdate = convertDate(model.birthdate);
            
            if (model != null && ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync("http://www.iranaspweb.ir/api/usersController", model);
            }

            return "user Added successfully.";
        }

        public async Task<string> users_update(user model)
        {
            if (model != null && ModelState.IsValid)
            {
                    var response = await client.PutAsJsonAsync("http://www.iranaspweb.ir/api/usersController" + "/" + model.Id, model);
            }
            return ("deleted successfully.");
        }

        public async Task<string> users_delete(user model)
        {
           var response = await client.DeleteAsync("http://www.iranaspweb.ir/api/usersController" + "/" + model.Id);
            return ("updated successfully ");
        }

       [HttpPost]
        public ActionResult SaveComments(string sdate,string edate)
        {
            //string sdate = "1397/03/06";
            //string edate = "1397/04/06";
            PersianDate ssdate = PersianDate.Parse(Convert.ToDateTime(sdate).ToString("yyyy/MM/dd HH:mm:ss"));
            DateTime Sdate = PersianDateConverter.ToGregorianDateTime(ssdate);
            PersianDate eedate = PersianDate.Parse(Convert.ToDateTime(edate).ToString("yyyy/MM/dd HH:mm:ss"));
            DateTime Edate = PersianDateConverter.ToGregorianDateTime(eedate);


            
            //PersianDate pd= PersianDateConverter.ToPersianDate(dt);

            var select = db.users.Where(q => q.birth >=Sdate && q.birth<=Edate).ToList();

            //foreach(var item in select)
            //{
                
            //    //dt.Add(Convert.ToDateTime(item));   
            //}

            return Content("");
        }


    }
}