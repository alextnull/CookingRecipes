using CookingRecipes.PageModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingRecipes.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage(RecipeDetailPageModel recipeDetailPageModel)
        {
            InitializeComponent();
            BindingContext = recipeDetailPageModel;
        }
    }
}