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
        public FormNewRecipe()
        {
            InitializeComponent();
        }

        private void FormNewRecipe_Load(object sender, EventArgs e)
        {

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var repository = new RecipeRepository();
            repository.Add(new Recipe
            {
                Title = textBox1.Text,
                Description = textBox2.Text
            });
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
