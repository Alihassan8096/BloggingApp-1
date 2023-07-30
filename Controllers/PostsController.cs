using BloggingApp.DAL;
using BloggingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BloggingApp.Controllers
{
    public class PostsController : ApiController
    {
        PostsDAL postDAL = new PostsDAL();
        UserDAL userDAL = new UserDAL();
        CategoriesDAL  categoriesDAL = new CategoriesDAL();

        // GET: api/Post
        public IList<Posts> Get()
        {
            try
            {
                var result = postDAL.GetAllPosts();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // GET: api/Post/5
        public Posts Get(int id)
        {
            Posts post = postDAL.GetPostByID(id);
            if (post.PostID > 0)
                return post;
            else
                return null;
        }

        // POST: api/Post
        public string Post(Posts newPost)
        {
            try
            {
                var userID = newPost.UserID;
                var categoryID = newPost.CategoryID;
                newPost.Timestamp = DateTime.Now;

                var user = userDAL.GetUserByID(userID);

                if(user.UserID == 0)
                {
                    return "User not Found! ";
                }

                var categories = categoriesDAL.GetCategoryByID(categoryID);

                if (categories.CategoryID == 0)
                {
                    return "Categories not Found! ";
                }
                var result = postDAL.InsertPost(newPost);
                if (result)
                    return "Post Created Successfully";

                return "Error Occured while creating Post. Try Again.";


            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Post/5
        public string Put(Posts updatePost)
        {
            try
            {
                var userID = updatePost.UserID;
                var categoryID = updatePost.CategoryID;
                var postID = updatePost.PostID;

                // Get Post from Database
                var post = postDAL.GetPostByID(postID);
                if (post.PostID == 0)
                    return "Post does not exist";

                if (!(post.UserID == updatePost.UserID))
                    return "User not allowed to Update Post";

                var category = categoriesDAL.GetCategoryByID(categoryID);
                if(category.CategoryID == 0)
                    return "Category Not found";

                var result = postDAL.UpdatePost(updatePost);
                if (result) return "Post Updated Successfully";
                return "Try Again. Error occured While updating Post";
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE: api/Category/5
        public string Delete(int id)
        {
            try
            {
                var postID = id;

                // Get Post from Database
                var post = postDAL.GetPostByID(postID);
                if (post.PostID == 0)
                    return "Post does not exist";


                var result = postDAL.DeletePost(id);
                if (result)
                {
                    return "Success! Post Deleted";
                }
                else return "error! Post Not Deleted";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
