using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783_Assignment2_C_.Controllers
{///
    public class J1Controller : ApiController
    {   /// <summary>
        /// This program receives 4 input which are options of a menu of a restaurent
        /// Out of which we will calculate the number of calories ordered by the customer(User i/p)
        /// So, we have 4 different arrays named "burgerOption", "drinkOption", "sideOption", "dessertOption"
        /// </summary>
        /// <param name="burger">The nth value of burger out of 4 options.</param>
        /// <param name="drink">The nth value of drink out of 4 options.</param>
        /// <param name="side">The nth value of side out of 4 options.</param>
        /// <param name="dessert">The nth value of dessert out of 4 options.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/J1/Menu/{burger}/{drink}/{side}/{dessert}")]
        
        public string Menu(int burger, int drink, int side, int dessert)

        {
            int[] burgerOption = { 461, 431, 420, 0 };
            int[] drinkOption = { 130, 160, 118, 0 };
            int[] sideOption = { 100, 57, 70, 0 };
            int[] dessertOption = { 167, 266, 75, 0 };

            burger = burgerOption[burger - 1];                  
            drink = drinkOption[drink - 1];
            side = sideOption[side - 1];
            dessert = dessertOption[dessert - 1];

            int total = burger + drink + side + dessert;

            return "Your Total calories is " + total;

        }
    }
}
