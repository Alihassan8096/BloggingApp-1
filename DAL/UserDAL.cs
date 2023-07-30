using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace BloggingApp.DAL
{
    public class UserDAL
    {
        // GET: api/Users
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["blogDB"].ToString();

        //getting all Users
        public List<Users> GetAllUsers()
        {
            List<Users> usersList = new List<Users>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getAllUsers";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable allUsers = new DataTable();

                connection.Open();
                sqlDataAdapter.Fill(allUsers);
                connection.Close();

                foreach (DataRow dataRow in allUsers.Rows)
                {
                    usersList.Add(new Users
                    {
                        UserID = Convert.ToInt32(dataRow["UserID"]),
                        Email = dataRow["Email"].ToString(),
                        Username = dataRow["Username"].ToString(),
                        Firstname = dataRow["Firstname"].ToString(),
                        Lastname = dataRow["Lastname"].ToString(),
                        DateOfBirth = (DateTime)dataRow["DateOfBirth"]
                    });
                }
            }

            return usersList;
        }

        //get user by ID
        public Users GetUserByID(int id)
        {
            Users user = new Users();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getUserByID";
                command.Parameters.AddWithValue("@UserID", id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable allUsers = new DataTable();

                connection.Open();
                sqlDataAdapter.Fill(allUsers);
                connection.Close();

                foreach (DataRow dataRow in allUsers.Rows)
                {
                    user.UserID = Convert.ToInt32(dataRow["UserID"]);
                    user.Email = dataRow["Email"].ToString();
                    user.Username = dataRow["Username"].ToString();
                    user.Firstname = dataRow["Firstname"].ToString();
                    user.Lastname = dataRow["Lastname"].ToString();
                    user.DateOfBirth = (DateTime)dataRow["DateOfBirth"];
                }
            }

            return user;
        }

        // Insert user
        public bool InsertUser(Users user)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "insertUser";
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Firstname", user.Firstname);
                command.Parameters.AddWithValue("@Lastname", user.Lastname);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);

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

        //update user
        public bool UpdateUser(Users user)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "updateUser";
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Firstname", user.Firstname);
                command.Parameters.AddWithValue("@Lastname", user.Lastname);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);

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

        //delete user
        public bool DeleteUser(int id)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deleteUser";
                command.Parameters.AddWithValue("@UserID", id);

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

        // Check if user exists
        public bool CheckUserExistence(string username, string email)
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "checkUserExistence";
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }

            // If count is greater than 0, user exists
            return count > 0;
        }

        // User login
        public bool LoginUser(string username, string password, out int userID)
        {
            int loginStatus;
            userID = 0; // Initialize with a default value

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "loginUser";
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                command.Parameters.Add("@LoginStatus", SqlDbType.Bit).Direction = ParameterDirection.Output;
                command.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();

                loginStatus = Convert.ToInt32(command.Parameters["@LoginStatus"].Value);
                if(loginStatus != 0) {
                    userID = Convert.ToInt32(command.Parameters["@UserID"].Value);
                }
                connection.Close();
            }

            // Return true for successful login, false for failed login
            return loginStatus == 1;
        }
    }
}