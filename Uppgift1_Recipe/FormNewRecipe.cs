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
    public partial class FormNewRecipe : Form
    {
        private RecipeRepository repository;
        BindingList<RecipeCategory> recipeCategory;
        private bool _isUpdate;
        private Recipe _recipe;
        private Recipe _updatedRecipe;
        private int _categoryIndex;
        private int _categoryId;

        public FormNewRecipe(Recipe recipe, bool isUpdate)
        {
            InitializeComponent();
            repository = new RecipeRepository();
            _isUpdate = isUpdate;
            _recipe = recipe;
            LoadCategories();
            if (isUpdate)
                LoadRecipe(recipe);
        }
        private void LoadRecipe(Recipe recipe)
        {
            var categories = repository.GetCategories().ToList();
            _categoryIndex = categories.FindIndex(c => c.Name == recipe.Category.Name) + 1;
            textBoxTitle.Text = recipe.Title;
            textBoxDesc.Text = recipe.Description;
            textBoxIng.Text = recipe.Ingredients;
            comboBoxCategory.SelectedIndex = _categoryIndex;

        }
        public void LoadCategories()
        {
            recipeCategory = new BindingList<RecipeCategory>(repository.GetCategories().ToList());
            recipeCategory.Insert(0, new RecipeCategory { Name = "" });
            comboBoxCategory.DataSource = recipeCategory;
            comboBoxCategory.DisplayMember = "Name";
        }
        private void FormNewRecipe_Load(object sender, EventArgs e)
        {
            //if (textBoxTitle.Text == String.Empty) !buttonOk.Enabled;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {         
            _updatedRecipe = new Recipe
            {
                RecipeId = _recipe.RecipeId,
                Title = textBoxTitle.Text,
                Description = textBoxDesc.Text,
                Ingredients = textBoxIng.Text,
                CategoryId = _categoryId

            };
            try
            {
                if (_isUpdate)
                    repository.Update(_updatedRecipe);
                else
                    repository.Add(_updatedRecipe);

                MessageBox.Show("Recipe saved!");
            }
            catch (Exception)
            {
                MessageBox.Show("Recipe could not save!");
            }
            Close();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var form = new RecipeMenu();
            form.Refresh();
            Close();
        }

        private void TextChanges(object sender, EventArgs e)
        {
            var categories = repository.GetCategories().ToList();
            _categoryId = ((RecipeCategory)comboBoxCategory.SelectedItem).CategoryId;
            if (textBoxTitle.Text == _recipe.Title && textBoxDesc.Text == _recipe.Description
                && textBoxIng.Text == _recipe.Ingredients && _categoryId == _categoryIndex)
            {
                buttonOk.Enabled = false;
            }
            else if (textBoxTitle.Text != String.Empty && textBoxDesc.Text != String.Empty && textBoxIng.Text != String.Empty && comboBoxCategory.Text != String.Empty)
                buttonOk.Enabled = true;
            else
            {
                DisableButtons();
            }
        }
        private void DisableButtons()
        {

            if (textBoxTitle.Text != String.Empty && textBoxDesc.Text != String.Empty && textBoxIng.Text != String.Empty && comboBoxCategory.Text != String.Empty)
                buttonOk.Enabled = true;
            else
                buttonOk.Enabled = false;

        }
    }
}
