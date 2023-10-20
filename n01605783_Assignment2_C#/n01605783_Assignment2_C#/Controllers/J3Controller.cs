using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783_Assignment2_C_.Controllers
{
    public class J3Controller : ApiController
    {
        /// <summary>
        /// Here are we taking 5 days input that a user can come on which day of event so we can get the data and according to that
        /// we can manage the most atteneded day with high facilities and services.
        /// if they enter "Y" or "y" so we consider that day.
        /// </summary>
        /// <param name="Day1">1st Day of event</param>
        /// <param name="Day2">2nd Day of event</param>
        /// <param name="Day3">3rd Day of event</param>
        /// <param name="Day4">4th Day of event</param>
        /// <param name="Day5">5th Day of event</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/J3/fiveDaysAttendence/{Day1}/{Day2}/{Day3}/{Day4}/{Day5}")]
        public string fiveDaysAttendence(string Day1, string Day2, string Day3, string Day4, string Day5)
        {
            string D1 = "", D2 = "", D3= "", D4 = "", D5 = "";
            if(Day1 == "Y" || Day1 == "y")
            {
                D1 = "Day1 on event";
            }
            if(Day2 == "Y" || Day2 == "y")
            {
                D2 = "Day2 on event";
            }
            if(Day3 == "Y" || Day3 == "y")
            {
                D3 = "Day3 on event";
            }
            if(Day4 == "Y" || Day4 == "y")
            {
                D4 = "Day4 on event";
            }
            if(Day5 == "Y" || Day5 == "y")
            {
                D5 = "Day5 on event";
            }
            return D1 + " " + D2 + " " + D3 + " " + D4 + " " + D5;
        }
    }
}
