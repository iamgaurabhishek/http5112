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
    public class CommentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/CommentData/ListComments
        // output a list of comment in system.
        [HttpGet]
        [Route("api/commentdata/listcomments")]
        public List<CommentDto> ListComments()
        {
            // Retrive all comments from the database
            List<Comment> Comments = db.Comments.ToList();

            // Create a list of store CommentDTO objects
            List<CommentDto> CommentDtos = new List<CommentDto>();
            // Transform each Comment object into CommentDtos and add to the list
            Comments.ForEach(
                comment => CommentDtos.Add(new CommentDto()
                {
                    CommentId = comment.CommentId,
                    CommentDescription = comment.CommentDescription, 
                    
                    UserId = comment.User.UserId,
                    UserName = comment.User.UserName,

                }));

            // return the list of CommentDto
            return CommentDtos;
        }


        /// <summary>
        /// Returns Comment base on id
        /// </summary>
        /// <param name="CommentId">The ID to find workout</param>
        /// <returns></returns>
        // GET: api/CommentData/FindComment/5
        [ResponseType(typeof(Comment))]
        [HttpGet]
        [Route("api/commentdata/findcomment/{id}")]
        public IHttpActionResult FindComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            // gets all the elements of Comment into CommentDto
            CommentDto CommentDto = new CommentDto()
            {
                CommentId = comment.CommentId,
                CommentDescription = comment.CommentDescription,

                UserId = comment.User.UserId,
                UserName = comment.User.UserName,

            };

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(CommentDto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        // POST: api/CommentData/UpdateComment/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/CommentData/UpdateComment/{id}")]
        public IHttpActionResult UpdateComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentId)
            {

                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/CommentData/AddComment
        [ResponseType(typeof(Comment))]
        [HttpPost]
        [Route("api/CommentData/AddComment")]
        public IHttpActionResult AddComment(Comment Comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(Comment);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/CommentData/DeleteComment/5
        [ResponseType(typeof(Comment))]
        [HttpPost]
        [Route("api/CommentData/DeleteComment/{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment Comment = db.Comments.Find(id);
            if (Comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(Comment);
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

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}