using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783_Assignment2_C_.Controllers
{
    public class J2Controller : ApiController
    {   /// <summary>
        /// here we are receiving two inputs from user of identify the number of sides of two dices
        /// So, we can calculate the possibility of the ways to get the sum of 10
        /// Like => in n = 12, m = 4 then the possible sums of 10 are 9+1, 8+2, 7+3, 6+4 in  total 4 ways to get the sum of 10.
        /// </summary>
        /// <param name="m">1st dice total numbers of side</param>
        /// <param name="n">2nd dice total numbers of side</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/J2/DiceGame/{m}/{n}")]
        public string DiceGame(int m, int n)

        {
            int count = 0;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (i + j == 10)
                    {
                        count++;
                    }
                }
            }
            return "There are " + count + " total ways to get the sum 10";
        }
    }
}
