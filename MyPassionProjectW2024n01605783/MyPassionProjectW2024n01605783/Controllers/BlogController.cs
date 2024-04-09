using MyPassionProjectW2024n01605783.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Description;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Script.Serialization;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace MyPassionProjectW2024n01605783.Controllers
{
    
    public class BlogController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static BlogController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/api/");
        }
        // GET: Blog/List
        public ActionResult List()
        {
            // receive information from the BlogDataController
            // receives a list of blogs by calling ListBlog Method.
            // semester 2
            // assume we only can talk to the API through an HTTP request...
            // ...using an HTTP client in C# to gather the Blog data.
            //

            // we have our http client object


            //set the path to the resource
            string url = "blogdata/listblogs";

            HttpResponseMessage response = client.GetAsync(url).Result;

            // we should try to digest this response into something we can use
            // digest it into an Blog data transfer object
            List<BlogDto> Blogs = response.Content.ReadAsAsync<List<BlogDto>>().Result;

            return View(Blogs);
        }
        public ActionResult Details(int id)
        {
            //objective: communicate with our Blog data api to retrieve one Blog
            //curl https://localhost:44301/api/blogdata/findblog/{id}

            string url = "blogdata/findblog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            BlogDto selectedblog = response.Content.ReadAsAsync<BlogDto>().Result;
            Debug.WriteLine("Blog to do : ");
            Debug.WriteLine(selectedblog.BlogHeading);


            return View(selectedblog);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Blog/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Blog.BlogName);
            //objective: add a new Blog into our system using the API
            //curl -H "Content-Type:application/json" -d @Blog.json https://localhost:44324/api/blogdata/addblog 
            string url = "blogdata/addblog";


            string jsonpayload = jss.Serialize(blog);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            
            return RedirectToAction("List");

        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the Blog information

            //objective: communicate with our Blog data api to retrieve one Blog
            //curl https://localhost:44324/api/blogdata/findblog/{id}

            string url = "blogdata/findblog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            BlogDto selectedblog = response.Content.ReadAsAsync<BlogDto>().Result;

            return View(selectedblog);
        }

        // POST: Blog/Update/5

        [HttpPost]
        [Route("Blog/Update/{id}")]
        public ActionResult Update(int id, Blog blog)
        {
            try
            {
                string url = "blogdata/Updateblog/" + id;
                //serialize into JSON
                string jsonpayload = jss.Serialize(blog);
                Debug.WriteLine(jsonpayload);
                //Send the request to the API
                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;

                // the below code is redirecting the page to "List" page.
                return RedirectToAction("List/" + id);
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "blogdata/findblog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            BlogDto selectedblog = response.Content.ReadAsAsync<BlogDto>().Result;
            return View(selectedblog);
        }

        // POST: Blog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "blogdata/deleteblog/" + id;
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