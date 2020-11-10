using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uppgift1_Recipe.Model;

namespace Uppgift1_Recipe
{
    public partial class Form1 : Form
    {
        private RecipeRepository repository;
        public Form1()
        {
            InitializeComponent();
            repository = new RecipeRepository();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text;
            string category = categoryTextBox.Text;
            var coll = repository.Search(title, category);
            ShowItems(coll);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //var recipes = repository.GetRecipe();
            //ShowItems(recipes);
        }
        private void ShowItems(IEnumerable<Recipe> recipes)
        {
            dataGridView1.DataSource = recipes.Select(r => new
            {
                Id = r.RecipeId,
                Titel = r.Title,
                Kategori = r.Category.Name
            }).ToArray();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var form = new FormNewRecipe();
            form.ShowDialog();
        }
    }
}
