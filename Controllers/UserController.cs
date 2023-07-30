using BloggingApp.DAL;
using BloggingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BloggingApp.Controllers
{
    public class UserController : ApiController
    {
        UserDAL userDAL = new UserDAL();
        PostsDAL postsDAL = new PostsDAL(); 
        private object user;

        // GET: api/Category
        public ActionResult<IList<Users>> Get()
        {
            try
            {
                var result = userDAL.GetAllUsers();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // GET: api/Category/5
        public Users Get(int id)
        {
            Users user = userDAL.GetUserByID(id);
            if (user.UserID > 0)
                return user;
            else
                return null;
        }

        // POST: api/Category
        public string Post(Users newUser)
        {
            try
            {
                var userExist = userDAL.CheckUserExistence(newUser.Username, newUser.Email);
                if (userExist)
                    return "User already rxist";
                var result = userDAL.InsertUser(newUser);
                if (result)
                {
                    return "success";
                }
                else return "error";
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Category/5
        public string Put(Users updateUser)
        {
            try
            {
                var result = userDAL.UpdateUser(updateUser);
                if (result)
                {
                    return "Success! User Updated";
                }
                else return "error! User Not Updated";
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
                var result = userDAL.DeleteUser(id);
                if (result)
                {
                    return "Success! User Deleted";
                }
                else return "error! User Not Deleted";
            }
            catch (Exception)
            {

                throw;
            }
        }

        // User login
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/User/login")]
        public object LoginUser(loginUser user)
        {
            int userId = 0;
            var result = userDAL.LoginUser(user.userName, user.password,out userId);
            if (!result || userId == 0)
                return new { error = "Can not Login in." };

            var getUser = userDAL.GetUserByID(userId);
            var posts = postsDAL.GetAllPosts(); 
            return new { message="User Successfully logged in", user = getUser, posts=posts};
        }
    }
}
