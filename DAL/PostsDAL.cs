using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BloggingApp.DAL
{
    public class PostsDAL
    {
        // GET: api/Posts
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["blogDB"].ToString();

        //getting all Posts
        public List<Posts> GetAllPosts()
        {
            List<Posts> usersList = new List<Posts>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getAllPosts";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable allPosts = new DataTable();

                connection.Open();
                sqlDataAdapter.Fill(allPosts);
                connection.Close();

                foreach (DataRow dataRow in allPosts.Rows)
                {
                    usersList.Add(new Posts
                    {
                        PostID = Convert.ToInt32(dataRow["PostID"]),
                        UserID = Convert.ToInt32(dataRow["UserID"]),
                        CategoryID = Convert.ToInt32(dataRow["CategoryID"]),
                        Title = dataRow["Title"].ToString(),
                        Content = dataRow["Content"].ToString(),
                        Timestamp = (DateTime)dataRow["Timestamp"]
                    });
                }
            }

            return usersList;
        }

        //get Post by ID
        public Posts GetPostByID(int id)
        {
            Posts post = new Posts();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getPostByID";
                command.Parameters.AddWithValue("@PostID", id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable allUsers = new DataTable();

                connection.Open();
                sqlDataAdapter.Fill(allUsers);
                connection.Close();

                foreach (DataRow dataRow in allUsers.Rows)
                {
                    post.PostID = Convert.ToInt32(dataRow["PostID"]);
                    post.UserID = Convert.ToInt32(dataRow["UserID"]);
                    post.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    post.Title = dataRow["Title"].ToString();
                    post.Content = dataRow["Content"].ToString();
                    post.Timestamp = (DateTime)dataRow["Timestamp"];
                }
            }

            return post;
        }

        // Insert Post
        public bool InsertPost(Posts post)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "insertPost";
                command.Parameters.AddWithValue("@UserID", post.UserID);
                command.Parameters.AddWithValue("@CategoryID", post.CategoryID);
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Content", post.Content);
                command.Parameters.AddWithValue("@Timestamp", post.Timestamp);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if (rowsAffected > 0)
            {
                return true;
            }
            else { return false; }

        }

        //update Post
        public bool UpdatePost(Posts post)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "updatePost";
                command.Parameters.AddWithValue("@PostID", post.PostID);
                command.Parameters.AddWithValue("@UserID", post.UserID);
                command.Parameters.AddWithValue("@CategoryID", post.CategoryID);
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Content", post.Content);
                command.Parameters.AddWithValue("@Timestamp", post.Timestamp);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if (rowsAffected > 0)
            {
                return true;
            }
            else { return false; }

        }

        //delete Post
        public bool DeletePost(int id)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deletePost";
                command.Parameters.AddWithValue("@PostID", id);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if (rowsAffected > 0)
            {
                return true;
            }
            else { return false; }

        }
    }
}