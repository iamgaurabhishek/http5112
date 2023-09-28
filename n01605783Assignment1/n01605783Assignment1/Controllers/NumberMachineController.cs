using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783Assignment1.Controllers
{
    public class NumberMachineController : ApiController
    {
        public string Get(int id)
        {
            int productOfTwo = id * 2;
            double powerToFour = Math.Pow(id, 4);
            int addToTen = id + 10;
            float remainderToTen = id % 10;
            return "Product of two : " + productOfTwo +" " + "Power of four : "+  powerToFour  + " " + " Remainder to ten : "+remainderToTen+ " "+ "Add to ten : " + addToTen;
        }
    }
}
