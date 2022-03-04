using Asp_net_timetable.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp_net_timetable.Controllers
{
    public class LoginController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "RcCQaBiJfpx6Nx2UFzCN4gFmdvwI5fkue3BMFMPL",
            BasePath = "https://asp-net-timetable-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient clientic;
        // GET: Login
        public ActionResult Index()
        {
            clientic = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = clientic.Get("Login");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Login>();
            foreach(var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Login>(((JProperty)item).Value.ToString()));
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Login login)
        {
            try
            {
                AddLoginToFirebase(login);
                ModelState.AddModelError(string.Empty, "Регистрация прошла успешно");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        private void AddLoginToFirebase(Login login)
        {
            clientic = new FireSharp.FirebaseClient(config);
            var data = login;
            PushResponse response = clientic.Push("Login", data);
            SetResponse setResponse = clientic.Set("Login" + data.tipeOfWeek, data);
        }
    }
}