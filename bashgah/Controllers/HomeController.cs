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

namespace bashgah.Controllers
{

    public class HomeController : Controller
    {
        public bcontext5 db = new bcontext5();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            //var selectjoin = from customers in db.customers
            //                 join registers in db.registers on customers.Id equals registers.CustomerId
            //                 select customers;
            //db.SaveChanges();
            // return View(selectjoin.ToList());
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //--------------------using join tables -------------------------------------------------------------------------
        [HttpGet]
        //public ActionResult test()
        //{

        //    //var selectjoin = from viewmodels in db.viewmodels join 
        //    //     customers in db.customers on viewmodels.Id equals customers.Id 
        //    //     join  registers in db.registers on customers.Id equals registers.CustomerId
        //    //                 select viewmodels;

        //    IEnumerable<viewmodel> selectjoin = (from e in db.customers.AsEnumerable()
        //                                         join j in db.registers.AsEnumerable() on e.Id equals j.CustomerId
        //                                         select new viewmodel()
        //                                         {
        //                                             Id = e.Id,
        //                                             name = e.name,
        //                                             family = e.family,
        //                                             Idd = j.Id,
        //                                             date = j.date,
        //                                             time = j.time

        //                                         });
        //    //var se = Mapper.Map<IEnumerable<customer>>(db.customers.ToList());
        //    return View(selectjoin);
        //}

        //[HttpGet]
        //public ActionResult create()
        //{

        //    return View();
        //}

        //------------------------------------ using auto mapper to map objects ---------------------------------------------
        [HttpPost]
        public ActionResult create(viewmodel model)
        {

            customer cust = new customer();
            Register reg = new Register();

            //customer c = Mapper.Map<customer>(model);
            //db.customers.Add(c);
            //db.SaveChanges();

            //Register r = Mapper.Map<Register>(model);
            //r = Mapper.Map<Register>(c);
            //r.CustomerId = c.Id;
            //db.registers.Add(r);
            //db.SaveChanges();


            customer cc = Mapper.Map<viewmodel, customer>(model);
            db.customers.Add(cc);
            db.SaveChanges();
            Register r1 = Mapper.Map<customer, Register>(cc);
            Register r2 = Mapper.Map<viewmodel, Register>(model);
            db.registers.Add(r1);
            db.registers.Add(r2);
            db.SaveChanges();


            return View();
        }
        //-----------------------------------------------calling web api crud -----------------------------------------------
        // Install-Package Microsoft.AspNet.WebApi.Client 
        static HttpClient client = new HttpClient();

        public async Task<ActionResult> getapi(int? id)
        {
            var response = await client.GetAsync("http://www.iranaspweb.ir/api/TodoController");

            var json = response.Content.ReadAsAsync<IEnumerable<TodoItem>>().Result.ToList();//serialize
            //TodoItem item = JsonConvert.DeserializeObject<TodoItem>(json);//deserilize
            var select = db.Todoitems.Where(q => q.Id == 2).FirstOrDefault();
            return View(json);
        }

        [HttpGet]
        public ActionResult postapi()
        {

            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> postapi(user model)
        {
            model.birthdate=convertDate(model.birthdate);
            var response = await client.PostAsJsonAsync("http://www.iranaspweb.ir/api/usersController", model);
            if(response.IsSuccessStatusCode == true)
            {

            }

            //string location = response.Headers.Location.ToString();
            return RedirectToAction("DetailUser");
        }

        public   ActionResult DetailUser()
        {
          //  var response = await client.GetAsync("http://www.iranaspweb.ir/api/usersController");

           // var json = response.Content.ReadAsAsync<IEnumerable<user>>().Result.ToList();//serialize
                    //TodoItem item = JsonConvert.DeserializeObject<TodoItem>(json);//deserilize
            return View();
           // return Json(json,JsonRequestBehavior.AllowGet); 
        }

     
        public async Task  <ActionResult> users_Read([DataSourceRequest] DataSourceRequest request)
        {
            var response = await client.GetAsync("http://www.iranaspweb.ir/api/usersController");
            var json = response.Content.ReadAsAsync<IEnumerable<user>>().Result.ToList();//serialize
            //TodoItem item = JsonConvert.DeserializeObject<TodoItem>(json);//deserilize
            // return Json(json,JsonRequestBehavior.AllowGet);
            
            DataSourceResult result = json.ToDataSourceResult(request);
            return Json(result);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task <ActionResult> users_create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable <user> users)
        {
            user model = new user();
            foreach (var item in users)
            {
                model.name = item.name;
                model.birthdate = item.birthdate;

            }
            var results = new List <viewmodel>();
            //model.birthdate = convertDate(model.birthdate);
           
            if (users != null && ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync("http://www.iranaspweb.ir/api/usersController", model);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task <ActionResult> users_update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<user> users)
        {
            if (users != null && ModelState.IsValid)
            {
                foreach (var model in users)
                {
                    var response = await client.PutAsJsonAsync("http://www.iranaspweb.ir/api/usersController" + "/" + model.Id, model);
                }
            }

            return Json(users.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task <ActionResult> users_delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<user> users)
        {
            if (users.Any())
            {
                foreach (var model in users)
                {
                    var response = await client.DeleteAsync("http://www.iranaspweb.ir/api/usersController" + "/" + model.Id);
                }
            }

            return Json(users.ToDataSourceResult(request, ModelState));
        }



        private static IEnumerable<viewmodel> getuser()
        {
            var db = new bcontext5();
            
            IEnumerable<viewmodel> select= (from p in db.users.AsEnumerable()
                                            select new viewmodel
                                            {
                                                name = p.name,
                                                birthdate = p.birthdate
                                            });
            return select;

        }

        [HttpGet]
        public ActionResult putapi(int id, string name, string birthdate)
        {
            user model = new user();
            model.Id = id;
            model.name = name;
            model.birthdate = birthdate;
            
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> putapi(user model)
        {
           // string now = p.Year.ToString() + "/" + p.Month.ToString() + "/" + p.Day.ToString().ToString(); // date time persian

            //string year = model.birthdate.Substring(0, 4);
            //string month = model.birthdate.Substring(6,1);
            //string day = model.birthdate.Substring(8, 2);
            //model.birthdate = year + "/" + month + "/" +day; 
            var response = await client.PutAsJsonAsync("http://www.iranaspweb.ir/api/usersController" + "/" + model.Id, model);
          
            model = await response.Content.ReadAsAsync<user>();
            return RedirectToAction("DetailUser");
        }

        [HttpGet]
        public ActionResult Delapi(int id, string name, string birthdate)
        {
            user model = new user();
            model.Id = id;
            model.name = name;
            model.birthdate = birthdate;
            return View(model);
        }

        [HttpPost]
        public async Task <ActionResult> Delapi(user model)
        {
            var response = await client.DeleteAsync("http://www.iranaspweb.ir/api/usersController" + "/" + model.Id);

            model = await response.Content.ReadAsAsync<user>();
            return RedirectToAction("DetailUser");
        }

        private string convertDate(string birthdate)
        {
            string year = birthdate.Substring(0, 4);
            string month = birthdate.Substring(6, 1);
            string day = birthdate.Substring(8, 2);
            birthdate = year + "/" + month + "/" + day;

            return birthdate;
        }
        public ActionResult getinfo()
        {

            return View();
        }


        public JsonResult getrecord()
        {
            var select = db.customers.ToList();
            return Json(select, JsonRequestBehavior.AllowGet);
        }

        public void sendmsg()
        {
            WhatsApp wa = new WhatsApp("989125152528", "your password", "Ali", false, false);
            wa.OnConnectSuccess += () =>
            {
                Response.Write("connect");
                wa.OnLoginSuccess += (phno, data) =>
                {
                    wa.SendMessage("to", "msg");
                };

                wa.OnLoginFailed += (data) =>
                {
                    Response.Write("login failed" + data);
                };
                wa.Login();
            };
        }
    }
}

