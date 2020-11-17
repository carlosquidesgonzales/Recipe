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
        private static List<RecipeCategory> _recipeCategories = new List<RecipeCategory>();
        private static List<Recipe> _recipe = new List<Recipe>();
        private string CS = "Data Source=localhost;Initial Catalog=SchoolProject;Integrated Security=SSPI;";

        public RecipeRepository()
        {
            _recipe.AddRange(GetRecipe());
            _recipeCategories.AddRange(GetCategories());

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
        public IEnumerable<RecipeCategory> GetCategories()
        {
            _recipe.Clear();
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var sql = $@"Select * from RecipeCategory";
                return con.Query<RecipeCategory>(sql);
            }
        }
        public IEnumerable<Recipe> GetRecipe()
        {
            _recipeCategories.Clear();
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var sql = $@"Select r.RecipeId, r.Title, r.Description, r.Ingredients, rc.CategoryId, rc.Name from Recipe r inner join RecipeCategory rc on r.CategoryId = rc.CategoryId";
                return con.Query<Recipe, RecipeCategory, Recipe>(sql, (recipe, category) =>
                {
                    recipe.Category = category;
                    return recipe;
                }, splitOn: "CategoryId");
            }
        }
        public Recipe GetById(int id)
        {
            return GetRecipe().FirstOrDefault(r => r.RecipeId == id);
        }



        public void Add(Recipe newRecipe)
        {
            string insertQuery = @"INSERT INTO [dbo].[Recipe]([Title], [Description], [Ingredients], [CategoryId])
                                VALUES
                               (@Title, @Description, @Ingredients ,@CategoryId)";
            using (SqlConnection db = new SqlConnection(CS))
            {
                db.Open();
                var result = db.Execute(insertQuery, new
                {
                    newRecipe.Title,
                    newRecipe.Description,
                    newRecipe.Ingredients,
                    newRecipe.CategoryId
                });
            }

        }

        public void Update(Recipe recipe)
        {
            using (SqlConnection db = new SqlConnection(CS))
            {
                string updateQuery = @"UPDATE[dbo].[Recipe]
                                   SET[Title] = @Title
                                      ,[Description] = @Description
                                      ,[Ingredients] = @Ingredients
                                      ,[CategoryId] = @CategoryId
                                  WHERE RecipeId = @Id";
                db.Open();
                db.Execute(updateQuery,
                    new
                    {
                        Id = recipe.RecipeId,
                        Title = recipe.Title,
                        Description = recipe.Description,
                        Ingredients = recipe.Ingredients,
                        CategoryId = recipe.CategoryId
                    }
                    );
            }
        }

        public void Remove(int receiptId)
        {
            Recipe recipe = GetById(receiptId);
            using (SqlConnection db = new SqlConnection(CS))
            {
                db.Open();
                db.Execute("DELETE FROM [dbo].[Recipe] WHERE RecipeId = @Id", new { Id = receiptId });
            }
        }


        public IEnumerable<Recipe> Search(string title, string catepory)
        {
            var recipe = GetRecipe().Where(r => r.Title.Contains(title) && r.Category.Name.Contains(catepory));
            return recipe;
        }
        public Recipe FindById(int id)
        {
            return _recipe.FirstOrDefault(r => r.RecipeId == id);
        }

        //public IEnumerable<Recipe> GetAll()
        //{
        //    return _receipts;
        //}
    }
}
