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
    public class TagController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TagController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/api/");
        }
        // GET: Tag/List
        public ActionResult List()
        {
            // receive information from the TagDataController
            // receives a list of tags by calling ListTags Method.
            // semester 2
            // assume we only can talk to the API through an HTTP request...
            // ...using an HTTP client in C# to gather the tag data.
            //

            // we have our http client object


            //set the path to the resource
            string url = "tagdata/listtags";

            HttpResponseMessage response = client.GetAsync(url).Result;

            // we should try to digest this response into something we can use
            // digest it into an tag data transfer object
            List<TagDto> Tags = response.Content.ReadAsAsync<List<TagDto>>().Result;

            return View(Tags);
        }

        public ActionResult Details(int id)
        {
            //objective: communicate with our Tag data api to retrieve one Tag
            //curl https://localhost:44301/api/tagdata/findtag/{id}

            string url = "tagdata/findtag/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            TagDto selectedtag = response.Content.ReadAsAsync<TagDto>().Result;
            Debug.WriteLine("Tag to do : ");
            Debug.WriteLine(selectedtag.TagName);


            return View(selectedtag);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Tag/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Tag.TagName);
            //objective: add a new Tag into our system using the API
            //curl -H "Content-Type:application/json" -d @Tag.json https://localhost:44324/api/tagdata/addtag 
            string url = "tagdata/addtag";


            string jsonpayload = jss.Serialize(tag);

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

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the tag information

            //objective: communicate with our tag data api to retrieve one tag
            //curl https://localhost:44324/api/tagdata/findtag/{id}

            string url = "tagdata/findtag/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            TagDto selectedtag = response.Content.ReadAsAsync<TagDto>().Result;

            return View(selectedtag);
        }

        // POST: Tag/Update/5

        [HttpPost]
        [Route("Tag/Update/{id}")]
        public ActionResult Update(int id, Tag tag)
        {
            try
            {
                string url = "tagdata/UpdateTag/" + id;
                //serialize into JSON
                string jsonpayload = jss.Serialize(tag);
                Debug.WriteLine(jsonpayload);
                //Send the request to the API
                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/TagData/UpdateTag/{id}
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


        // GET: Tag/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "tagdata/findtag/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TagDto selectedtag = response.Content.ReadAsAsync<TagDto>().Result;
            return View(selectedtag);
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "tagdata/deletetag/" + id;
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