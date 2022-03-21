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
    public class RegistrationController : Controller
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
            FirebaseResponse response = clientic.Get("Registration/");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            //var array = new string[100];
            var list = new List<Login>();

            //for (int i = 0; i < data; i++)
            //{
            //    list.Add(JsonConvert.DeserializeObject<Login>(((JProperty)array[i]).Value.ToString()));
            //}

            foreach (var item in data)
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
        public ActionResult Create(Registration registration)
        {
            try
            {
                AddLoginToFirebase(registration);             
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        //private async void AddLoginToFirebaseSecond(Registration registration)
        //{
        //    //clientic = new FireSharp.FirebaseClient(config);
        //    //FirebaseResponse firebaseResponse = await clientic.GetAsync("Login");
        //    //string jsonText = firebaseResponse.Body;
        //    //if (jsonText?.Length > 0)
        //    //{
        //    //    Dictionary<string, Login> clients = JsonConvert.DeserializeObject<Dictionary<string, Login>>(jsonText);
        //    //    // данные получены, можно использовать
        //    //    foreach (KeyValuePair<string, Login> pair in clients)
        //    //    {
                    
        //    //    }
        //    //}




        //    //clientic = new FireSharp.FirebaseClient(config);
        //    //dynamic newdata = registration;
        //    //// string email_string = Convert.ToString(Index());

        //    ////считать из базы
        //    //clientic = new FireSharp.FirebaseClient(config);
        //    //FirebaseResponse response = clientic.Get("Login");
        //    //dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

        //    ////string[] myArrayOfInts = data.values.ToObject<string[]>();

        //    ////var em = data.email;
        //    ////var json = JsonConvert.DeserializeObject<Registration[]>(data);

        //    //var list = new List<Login>();

        //    //string ah;
        //    //foreach (var item in data)
        //    //{

        //    //    ah = item;
        //    //    //list.Add(JsonConvert.DeserializeObject<Login>(((JProperty)item).Value.ToString()));
        //    //}

        //    //dynamic results = JsonConvert.DeserializeObject<dynamic>(data);
        //    //var id = results.email;
        //    //var name = results.Name;

        //    //try
        //    //{
        //    //    JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(data);
        //    //    JToken json_token = jObject["json"][0];//токен на массив массивов

        //    //    var players = json_token["Login"].ToArray();

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    ModelState.AddModelError(string.Empty, ex.Message);
        //    //}

        //    //PushResponse newresponse = clientic.Push("Registration/", newdata);
        //    //SetResponse setResponse = clientic.Set("Registration/" + newdata.email, newdata);

        //    //return View(list);
        //}

        private void AddLoginToFirebase(Registration registration)
        {
            clientic = new FireSharp.FirebaseClient(config);
            var data = registration;
            //string list = Convert.ToString(Index());

            //if (data.email == list)
            //    ModelState.AddModelError(string.Empty, "Данный пользователь существует");
            //else
            
            PushResponse response = clientic.Push("Registration/", data);
            SetResponse setResponse = clientic.Set("Registration/" + data.email, data);
            
        }
    }
}