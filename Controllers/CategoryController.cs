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
    public class CategoryController : ApiController
    {
        CategoriesDAL categoryDAL = new CategoriesDAL();
        private object category;

        // GET: api/Category
        public ActionResult<IList<Category>> Get()
        {
            try
            {
                var result = categoryDAL.GetAllCategories();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // GET: api/Category/5
        public Category Get(int id)
        {
            Category category = categoryDAL.GetCategoryByID(id);
            if(category.CategoryID > 0)
                return category;
            else
                return null;
          }

        // POST: api/Category
        public string Post(Category newCategory)
        {
            try
            {
                newCategory.CreationDate = DateTime.Now;
                var result = categoryDAL.InsertCategory(newCategory);
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
        public string Put(Category updateCategory)
        {
            try
            {
                var result = categoryDAL.UpdateCategory(updateCategory);
                if (result)
                {
                    return "Success! Category Updated";
                }
                else return "error! Category Not Updated";
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
                var result = categoryDAL.DeleteCategory(id);
                if (result)
                {
                    return "Success! Category Deleted";
                }
                else return "error! Category Not Deleted";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
