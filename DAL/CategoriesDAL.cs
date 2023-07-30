using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.DAL
{
    public class CategoriesDAL
    {
        // GET: api/Category
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["blogDB"].ToString();

        //getting all categories
        public List<Category> GetAllCategories()
        {
            List<Category> categoriesList = new List<Category>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getAllCategories";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable allCategories = new DataTable();

                connection.Open();
                sqlDataAdapter.Fill(allCategories);
                connection.Close();

                foreach (DataRow dataRow in allCategories.Rows)
                {
                    categoriesList.Add(new Category
                    {
                        CategoryID = Convert.ToInt32(dataRow["CategoryID"]),
                        CategoryName = dataRow["CategoryName"].ToString(),
                        Description = dataRow["Description"].ToString(),
                        CreationDate = (DateTime)dataRow["CreationDate"]
                    });
                }
            }

            return categoriesList;
        }

        //get category by ID
        public Category GetCategoryByID(int id)
        {
            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getCategoryByID";
                command.Parameters.AddWithValue("@CategoryID", id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable allCategories = new DataTable();

                connection.Open();
                sqlDataAdapter.Fill(allCategories);
                connection.Close();

                foreach (DataRow dataRow in allCategories.Rows)
                {
                    category.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    category.CategoryName = dataRow["CategoryName"].ToString();
                    category.Description = dataRow["Description"].ToString();
                    category.CreationDate = (DateTime)dataRow["CreationDate"];
                }
            }

            return category;
        }

        // Insert Category
        public bool InsertCategory(Category category)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "insertCategory";
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.Parameters.AddWithValue("@Description", category.Description);
                command.Parameters.AddWithValue("@CreationDate", category.CreationDate);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if(rowsAffected > 0)
            {
                return true;
            }
            else { return false; }

        }

        //update category
        public bool UpdateCategory(Category category)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "updateCategory";
                command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.Parameters.AddWithValue("@Description", category.Description);
                command.Parameters.AddWithValue("@CreationDate", category.CreationDate);

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

        //delete category
        public bool DeleteCategory(int id)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeleteCategory";
                command.Parameters.AddWithValue("@CategoryID", id);

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