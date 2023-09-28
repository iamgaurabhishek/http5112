using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783Assignment1.Controllers
{
    public class HostingCostController : ApiController
    {
        public string Get(int id) // 28
        {
            // First taking the input to calculate the fortnight
            int fortnightsCal = id / 14; // 28/14 
            // HST tax cost value
            double hst = 0.13;
            // Cost of per fortnight
            double fortnightCost = 5.50;

            // increasing the fortnight by 1
            fortnightsCal = fortnightsCal + 1; // 2 + 1 = 3
            double totalFortnighCost = fortnightsCal * fortnightCost; // 3 * 5.50 = 16.50
            // calculating the total tax 
            double totalHst = hst * totalFortnighCost; // 0.13 * 16.50 = 2.14

            double totalCostPerFortnight = totalFortnighCost+ totalHst; // 16.50 + 2.14 
            // ToString("F2") is used to format the floating-point numbers with two decimal places.
            return fortnightsCal+" fortnights at $ 5.50 = $ "+ totalFortnighCost.ToString("F2") + "CAD\n" +
                "HST 13 % = $ " + totalHst.ToString("F2") + " CAD\n" +
                "Total = $ " + totalCostPerFortnight.ToString("F2") + " CAD"; 
        }
    } 
}
