using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783Assignment1.Controllers
{
    public class GreetingController : ApiController
    {
        //[HttpPost]
        //[Route("/api/Greeting/")]
        //public string Greeting()
        //{
          //  return "Hello World!";
        //}
        public string Get(string id)
        {
            return "Greeting to " + id + " people!";
        }
        public string Post()
        {
            return "Hello World!";
        }
    }
}
