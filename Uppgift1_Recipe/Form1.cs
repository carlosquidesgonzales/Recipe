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
    public partial class RecipeMenu : Form
    {
        private RecipeRepository repository = new RecipeRepository();
        private IEnumerable<Recipe> _recipes => repository.GetRecipe();
        BindingList<RecipeCategory> recipeCategory;
        public RecipeMenu()
        {
            InitializeComponent();
            LoadCategories();

        }
        public void LoadCategories() 
        {      
            recipeCategory = new BindingList<RecipeCategory>(repository.GetCategories().ToList());
            recipeCategory.Insert(0, new RecipeCategory { Name = "" });
            comboBoxCategory.DataSource = recipeCategory;
            comboBoxCategory.DisplayMember = "Name";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            listViewRecipes.Items.Clear();
            DisableButtons();
            ShowListView();
        }
        private void ShowListView()
        {
            string title = titleTextBox.Text;
            var category = ((RecipeCategory)comboBoxCategory.SelectedItem).Name;
            var coll = repository.Search(title, category).ToList();
            foreach (var recipe in coll)
            {
                var item = new ListViewItem(new string[] { recipe.RecipeId.ToString(), recipe.Title, recipe.Category.Name });
                listViewRecipes.Items.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listViewRecipes.Items.Clear();
            //    repository = new RecipeRepository();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Recipe recipe = new Recipe();
            var form = new FormNewRecipe(recipe, false);
            form.ShowDialog();
        }
  
        private void DisableButtons()
        {
            bool selected = listViewRecipes.SelectedItems.Count == 1;
            if (selected)
            {
                buttonDelete.Enabled = true;
                buttonUpdate.Enabled = true;
            }
            else
            {
                buttonDelete.Enabled = false;
                buttonUpdate.Enabled = false;
            }
        }
        private void ClickGridView(object sender, EventArgs e)
        {
            DisableButtons();
            var itemId = "";
            
            foreach (DataGridViewRow item in dataGridViewRecipes.SelectedRows)
            {
                MessageBox.Show($"{item.Cells[0].Value} {item.Cells[1].Value} {item.Cells[2].Value}");
                itemId = item.Cells[0].Value.ToString();
            }    
            
        }

        private void listViewRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
                DisableButtons();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(listViewRecipes.SelectedItems[0].Text);
            try
            {
                repository.Remove(id);
                MessageBox.Show("Recipe deleted!");
                listViewRecipes.Items.Clear();
                DisableButtons();
            }
            catch (Exception)
            {
                MessageBox.Show("Recipe could not delete!");
            }          
            ShowListView();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(listViewRecipes.SelectedItems[0].Text);
            Recipe recipe = repository.GetById(id);
            var form = new FormNewRecipe(recipe, true);
            listViewRecipes.Items.Clear();
            form.ShowDialog();
            DisableButtons();
        }

       
    }
}
