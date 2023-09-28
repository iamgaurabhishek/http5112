using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783Assignment1.Controllers
{
    public class AddTenController : ApiController
    {
        public int Get(int id)
        {
            return id + 10;
        }
    }
}
