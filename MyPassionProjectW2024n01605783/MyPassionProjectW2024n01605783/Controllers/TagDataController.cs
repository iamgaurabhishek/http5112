using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyPassionProjectW2024n01605783.Models;

namespace MyPassionProjectW2024n01605783.Controllers
{
    public class TagDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/TagData/ListTags
        // output a list of tag in system.
        [HttpGet]
        [Route("api/tagdata/listtags")]
        public List<TagDto> ListTags()
        {
            // Retrive all tags from the database
            List<Tag> Tags = db.Tags.ToList();

            // Create a list of store TagDTO objects
            List<TagDto> TagDtos = new List<TagDto>();
            // Transform each Tag object into TagDtos and add to the list
            Tags.ForEach(
                tag => TagDtos.Add(new TagDto()
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName,

                }));

            // return the list of TagDto
            return TagDtos;
        }


        /// <summary>
        /// Returns Tag base on id
        /// </summary>
        /// <param name="TagId">The ID to find workout</param>
        /// <returns></returns>
        // GET: api/TagData/FindTag/5
        [ResponseType(typeof(Tag))]
        [HttpGet]
        [Route("api/tagdata/findtag/{id}")]
        public IHttpActionResult FindTag(int id)
        {
            Tag tag = db.Tags.Find(id);
            // gets all the elements of Tag into TagDto
            TagDto TagDto = new TagDto()
            {
                TagId = tag.TagId,
                TagName = tag.TagName,

            };

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(TagDto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        // POST: api/TagData/UpdateTag/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/TagData/UpdateTag/{id}")]
        public IHttpActionResult UpdateTag(int id, Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tag.TagId)
            {

                return BadRequest();
            }

            db.Entry(tag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TagData/AddTag
        [ResponseType(typeof(Tag))]
        [HttpPost]
        [Route("api/TagData/AddTag")]
        public IHttpActionResult AddTag(Tag Tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tags.Add(Tag);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/TagData/DeleteTag/5
        [ResponseType(typeof(Tag))]
        [HttpPost]
        [Route("api/TagData/DeleteTag/{id}")]
        public IHttpActionResult DeleteTag(int id)
        {
            Tag Tag = db.Tags.Find(id);
            if (Tag == null)
            {
                return NotFound();
            }

            db.Tags.Remove(Tag);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TagExists(int id)
        {
            return db.Tags.Count(e => e.TagId == id) > 0;
        }
    }
}