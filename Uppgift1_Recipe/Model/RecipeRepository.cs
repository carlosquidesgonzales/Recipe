using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift1_Recipe.Model
{
    public class RecipeRepository
    {
        private static List<RecipeCategory> _receiptCategories = new List<RecipeCategory>();
        private static List<Recipe> _recipe = new List<Recipe>();
        private string CS = "Data Source=localhost;Initial Catalog=SchoolProject;Integrated Security=SSPI;";

        public RecipeRepository()
        {
            var recipes = GetRecipe();
            GetCategories();
            _recipe.AddRange(recipes);
            //if (_recipe == null)
            //{              
            //    _receiptCategories = new List<RecipeCategory>
            //    {
            //        new RecipeCategory { CategoryId = 1, Name = "Asiatiskt"},
            //        new RecipeCategory { CategoryId = 2, Name = "Husmanskost"},
            //    };
            //    _recipe = new List<Recipe>
            //    {
            //        new Recipe {RecipeId = 1, Category = _receiptCategories.First(e=>e.CategoryId == 1), Title = "Sushi", Ingredients = "Fisk, soja"},
            //        new Recipe { RecipeId = 2, Category = _receiptCategories.First(e => e.CategoryId == 2), Title = "Pannkakor", Ingredients = "Mjölk,mjöl,ägg,smör,sylt" },
            //        new Recipe { RecipeId = 3, Category = _receiptCategories.First(e => e.CategoryId == 1), Title = "Yakiniku", Ingredients = "Ris,entrocote,soja" }
            //    };
            //}
        }
        public void GetCategories()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var sql = $@"Select * from RecipeCategory";
                var re = con.Query<RecipeCategory>(sql);

                _receiptCategories.AddRange(re);
            }
        }
        public IEnumerable<Recipe> GetRecipe()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var sql = $@"Select r.RecipeId, r.Title, r.Description, r.Ingredients, rc.CategoryId, rc.Name from Recipe r inner join RecipeCategory rc on r.CategoryId = rc.CategoryId";
                var re = con.Query<Recipe, RecipeCategory, Recipe>(sql, (recipe, category) => {
                    recipe.Category = category;
                    return recipe;
                }, splitOn: "CategoryId");
                return re;
            }
        }
        public Recipe GetById(int id)
        {
            return _recipe.FirstOrDefault(r => r.RecipeId == id);
        }



        public void Add(Recipe newReceipt)
        {
            newReceipt.RecipeId = _recipe.Max(r => r.RecipeId) + 1;
            if (newReceipt.Category == null)
                newReceipt.Category = _receiptCategories.First();
            _recipe.Add(newReceipt);
        }

        public void Update(Recipe receipt)
        {

        }

        public void Remove(int receiptId)
        {

        }


        public IEnumerable<Recipe> Search(string title, string catepory)
        {
            return _recipe.Where(r => r.Title.Contains(title));
        }



        //public IEnumerable<Recipe> GetAll()
        //{
        //    return _receipts;
        //}
    }
}
