using MyPassionProjectW2024n01605783.Models;
using MyPassionProjectW2024n01605783.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace MyPassionProjectW2024n01605783.Controllers
{
    public class ExerciseController : Controller
    {
        
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ExerciseController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/api/");
        }
        // GET: Exercise/List
        public ActionResult List()
        {
            // receive information from the ExerciseDataController
            // receives a list of exercises by calling ListExercises Method.
            // semester 2
            // assume we only can talk to the API through an HTTP request...
            // ...using an HTTP client in C# to gather the exercise data.
            //
            
            // we have our http client object
            

            //set the path to the resource
            string url = "exercisedata/listexercises";

            HttpResponseMessage response = client.GetAsync(url).Result;

            // we should try to digest this response into something we can use
            // digest it into an exercise data transfer object
            List<ExerciseDto> Exercises = response.Content.ReadAsAsync<List<ExerciseDto>>().Result;

            return View(Exercises);
        }

        public ActionResult Details(int id)
        {
            DetailsExercise ViewModel = new DetailsExercise();
            //objective: communicate with our Exercise data api to retrieve one Exercise
            //curl https://localhost:44301/api/exercisedata/findexercise/{id}

            string url = "exercisedata/findexercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ExerciseDto selectedexercise = response.Content.ReadAsAsync<ExerciseDto>().Result;

            ViewModel.SelectedExercise = selectedexercise;

            url = "BlogData/ListBlogsForExercise/" + id;
            response = client.GetAsync(url).Result;
            List<BlogDto> blogsForExercise = response.Content.ReadAsAsync<List<BlogDto>>().Result;
            ViewModel.BlogsForExercise = blogsForExercise;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Exercise/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Exercise/Create
        [HttpPost]
        public ActionResult Create(Exercise exercise)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Exercise.ExerciseName);
            //objective: add a new Exercise into our system using the API
            //curl -H "Content-Type:application/json" -d @Exercise.json https://localhost:44324/api/exercisedata/addexercise 
            string url = "exercisedata/addexercise";


            string jsonpayload = jss.Serialize(exercise);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Exercise/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the animal information

            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44324/api/animaldata/findanimal/{id}

            string url = "exercisedata/findexercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            ExerciseDto selectedexercise = response.Content.ReadAsAsync<ExerciseDto>().Result;      

            return View(selectedexercise);
        }

        // POST: Exercise/Update/5
        
        [HttpPost]
        [Route("Exercise/Update/{id}")]
        public ActionResult Update(int id, Exercise exercise)
        {
            try
            {
                string url = "exercisedata/UpdateExercise/" + id;
                //serialize into JSON
                string jsonpayload = jss.Serialize(exercise);
                Debug.WriteLine(jsonpayload);
                //Send the request to the API
                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/ExerciseData/UpdateExercise/{id}
                //Header : Content-Type: application/json
            // the below code is like sending the fetch request to the url point which is defined by us
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                
                // the below code is redirecting the page to "List" page.
                return RedirectToAction("List/" + id);
            }
            catch
            {
                return View();
            }
        }


        // GET: Exercise/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "exercisedata/findexercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ExerciseDto selectedexercise = response.Content.ReadAsAsync<ExerciseDto>().Result;
            return View(selectedexercise);
        }

        // POST: Exercise/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "exercisedata/deleteexercise/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}