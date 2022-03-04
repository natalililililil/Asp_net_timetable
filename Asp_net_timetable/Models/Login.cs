using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_net_timetable.Models
{
    public class Login
    {
        public string tipeOfWeek { get; set; }
        public string dayOfWeek { get; set; }
        public string time { get; set; }
        public string subject { get; set; }
        public string teacher { get; set; }
        public string lecture_room { get; set; }
    }
}