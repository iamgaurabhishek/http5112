using MyPassionProjectW2024n01605783.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyPassionProjectW2024n01605783.Controllers
{
    public class CommentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CommentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/api/");
        }
        // GET: Comment/List
        public ActionResult List()
        {
            // receive information from the CommentDataController
            // receives a list of comments by calling ListComments Method.
            // semester 2
            // assume we only can talk to the API through an HTTP request...
            // ...using an HTTP client in C# to gather the comment data.
            //

            // we have our http client object


            //set the path to the resource
            string url = "commentdata/listcomments";

            HttpResponseMessage response = client.GetAsync(url).Result;

            // we should try to digest this response into something we can use
            // digest it into an comment data transfer object
            List<CommentDto> Comments = response.Content.ReadAsAsync<List<CommentDto>>().Result;

            return View(Comments);
        }

        public ActionResult Details(int id)
        {
            //objective: communicate with our Comment data api to retrieve one Comment
            //curl https://localhost:44301/api/commentdata/findcomment/{id}

            string url = "commentdata/findcomment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            CommentDto selectedcomment = response.Content.ReadAsAsync<CommentDto>().Result;
            Debug.WriteLine("Comment to do : ");


            return View(selectedcomment);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Comment/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Comment.CommentName);
            //objective: add a new Comment into our system using the API
            //curl -H "Content-Type:application/json" -d @Comment.json https://localhost:44324/api/exercisedata/addexercise 
            string url = "exercisedata/addexercise";


            string jsonpayload = jss.Serialize(comment);

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

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the comment information

            //objective: communicate with our comment data api to retrieve one comment
            //curl https://localhost:44324/api/commentdata/findcomment/{id}

            string url = "exercisedata/findexercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            CommentDto selectedexercise = response.Content.ReadAsAsync<CommentDto>().Result;

            return View(selectedexercise);
        }

        // POST: Comment/Update/5

        [HttpPost]
        [Route("Comment/Update/{id}")]
        public ActionResult Update(int id, Comment exercise)
        {
            try
            {
                string url = "exercisedata/UpdateComment/" + id;
                //serialize into JSON
                string jsonpayload = jss.Serialize(exercise);
                Debug.WriteLine(jsonpayload);
                //Send the request to the API
                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/CommentData/UpdateComment/{id}
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


        // GET: Comment/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "exercisedata/findexercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CommentDto selectedexercise = response.Content.ReadAsAsync<CommentDto>().Result;
            return View(selectedexercise);
        }

        // POST: Comment/Delete/5
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