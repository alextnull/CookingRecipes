using CookingRecipes.PageModels;
using Xamarin.Forms;

namespace CookingRecipes.Pages
{
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage(RecipesPageModel recipesPageModel)
        {
            InitializeComponent();
            BindingContext = recipesPageModel;
        }
    }
}
