using BloggingApp.DAL;
using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BloggingApp.Controllers
{
    public class CommentController : ApiController
    {
        // GET: api/post
        CommentsDAL commentDAL = new CommentsDAL();

        [HttpGet]
        [Route("api/Post/{postID}/comments")]
        public IList<Comments> GetAllCommentsByPostID(int postID)
        {
            var comments = commentDAL.GetCommentsByPostID(postID);
            return comments;
        }
      
        // POST: api/Comment
        public string Post(Comments comment)
        {
            try
            {
                var result = commentDAL.AddComment(comment);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE: api/post/5
        public string Delete(int id)
        {
            try
            {
                var result = commentDAL.deleteComment(id);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
