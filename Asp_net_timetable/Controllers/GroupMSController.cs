using Asp_net_timetable.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Asp_net_timetable.Controllers
{
    public class GroupMSController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "RcCQaBiJfpx6Nx2UFzCN4gFmdvwI5fkue3BMFMPL",
            BasePath = "https://asp-net-timetable-default-rtdb.europe-west1.firebasedatabase.app"
        };

        IFirebaseClient infa;
        // GET: Login

        public ActionResult Index()
        {
            infa = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = infa.Get("GroupMS_Нижняя_Первая/");
            //FirebaseResponse response = infa.Get(data.dayOfWeek + " / " + data.time);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<GroupMSModel>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<GroupMSModel>(((JProperty)item).Value.ToString()));
            }
            list.Reverse();
            return View(list);
        }       

        public ActionResult IndexTop()
        {
            infa = new FireSharp.FirebaseClient(config);
            //var link = groupMS;
            FirebaseResponse response = infa.Get("GroupMS_Верхняя_Первая/");
            //FirebaseResponse response = infa.Get(data.dayOfWeek + " / " + data.time);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<GroupMSModel>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<GroupMSModel>(((JProperty)item).Value.ToString()));
            }
            list.Reverse();
            return View(list);
        }

        public ActionResult Index_Second()
        {
            infa = new FireSharp.FirebaseClient(config);
            //var link = groupMS;
            FirebaseResponse response = infa.Get("GroupMS_Нижняя_Вторая/");
            //FirebaseResponse response = infa.Get(data.dayOfWeek + " / " + data.time);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<GroupMSModel>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<GroupMSModel>(((JProperty)item).Value.ToString()));
            }
            list.Reverse();
            return View(list);
        }

        public ActionResult Index_Top_Second()
        {
            infa = new FireSharp.FirebaseClient(config);
            //var link = groupMS;
            FirebaseResponse response = infa.Get("GroupMS_Верхняя_Вторая/");
            //FirebaseResponse response = infa.Get(data.dayOfWeek + " / " + data.time);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<GroupMSModel>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<GroupMSModel>(((JProperty)item).Value.ToString()));
            }
            list.Reverse();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GroupMSModel groupMS)
        {
            try
            {
                AddDataToFirebase(groupMS);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        private void AddDataToFirebase(GroupMSModel groupMS)
        {
            infa = new FireSharp.FirebaseClient(config);
            var data = groupMS;
            int day_id = 0, time_id = 0;
            string link;

            switch (Convert.ToString(data.dayOfWeek))
            {
                case "Понедельник":
                    day_id = 6;
                    break;
                case "Вторник":
                    day_id = 5;
                    break;
                case "Среда":
                    day_id = 4;
                    break;
                case "Четверг":
                    day_id = 3;
                    break;
                case "Пятница":
                    day_id = 2;
                    break;
                case "Суббота":
                    day_id = 1;
                    break;
            }

            switch (Convert.ToString(data.time))
            {
                case "9:00 - 10:20":
                    time_id = 6;
                    break;
                case "10:40 - 12:00":
                    time_id = 5;
                    break;
                case "12:10 - 13:30":
                    time_id = 4;
                    break;
                case "14:00 - 15:20":
                    time_id = 3;
                    break;
                default:
                    time_id = 2;
                    break;
            }

            data.id = data.tipeOfWeek + "_" + day_id + "_" + time_id + "_" + data.subgroup;

            if (Convert.ToString(data.tipeOfWeek) == "Нижняя")
            {
                if (Convert.ToString(data.subgroup) == "Первая")
                    link = "GroupMS_Нижняя_Первая/";
                else
                    link = "GroupMS_Нижняя_Вторая/";
            }
            else
            {
                if (Convert.ToString(data.subgroup) == "Первая")
                    link = "GroupMS_Верхняя_Первая/";
                else
                    link = "GroupMS_Верхняя_Вторая/";
            }                    

            PushResponse response = infa.Push(link + data.id, data);
            //data.id = response.Result.name;

            //добавляет всю инфу из класса, название раздела будет понедельник
            SetResponse setResponse = infa.Set(link + data.id, data);
        }

        public ActionResult GeneratePdf()
        {
            return new Rotativa.ActionAsPdf("Index");
        }

        public ActionResult GeneratePdfTop()
        {
            return new Rotativa.ActionAsPdf("IndexTop");
        }

        public ActionResult GeneratePdfSecond()
        {
            return new Rotativa.ActionAsPdf("Index_Second");
        }

        public ActionResult GeneratePdfTopSecond()
        {
            return new Rotativa.ActionAsPdf("Index_Top_Second");
        }
    }
}
