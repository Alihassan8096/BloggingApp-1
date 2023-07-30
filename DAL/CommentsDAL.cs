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
    public class CommentsDAL
    {
        // GET: api/Category
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["blogDB"].ToString();

        readonly PostsDAL postDal = new PostsDAL();
        private static readonly UserDAL userDAL = new UserDAL();
        readonly UserDAL UserDAL = userDAL;

        //returns all comments on a single post
        public List<Comments> GetCommentsByPostID(int postID)
        {
            List<Comments> commentsList = new List<Comments>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getCommentsByPostID";
                command.Parameters.AddWithValue("@PostID", postID);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable commentsTable = new DataTable();

                connection.Open();
                dataAdapter.Fill(commentsTable);
                connection.Close();

                foreach (DataRow dataRow in commentsTable.Rows)
                {
                    commentsList.Add(new Comments
                    {
                        CommentID = Convert.ToInt32(dataRow["CommentID"]),
                        PostID = Convert.ToInt32(dataRow["PostID"]),
                        UserID = Convert.ToInt32(dataRow["UserID"]),
                        CommentText = dataRow["CommentText"].ToString(),
                        Timestamp = Convert.ToDateTime(dataRow["Timestamp"])
                    });
                }
            }

            return commentsList;
        }

        public string AddComment(Comments comment)
        {
            var Post = postDal.GetPostByID(comment.PostID);
            if (Post.PostID == 0)
                return "The Post You are trying to add comment does'nt exist!";

            var User = UserDAL.GetUserByID(Post.UserID);
            if (User == null) 
                return "The comment can't be added because user does not exist";

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "insertComment";
                command.Parameters.AddWithValue("@UserID", comment.UserID);
                command.Parameters.AddWithValue("@PostID", comment.PostID);
                command.Parameters.AddWithValue("@CommentText", comment.CommentText);
                command.Parameters.AddWithValue("@Timestamp", comment.Timestamp);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if (rowsAffected > 0)
            {
                return "Comment Added Successfully";
            }
            else { return "Unable to add Comment"; }
        }

        public string deleteComment(int id)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deleteComment";
                command.Parameters.AddWithValue("@CommentID", id);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if (rowsAffected > 0)
            {
                return "Comment Deleted";
            }   
            else { return "Comment Not Deleted"; }
        }
    }
}