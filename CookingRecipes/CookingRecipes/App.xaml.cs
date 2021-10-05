using CookingRecipes.Data;
using CookingRecipes.PageModels;
using CookingRecipes.Pages;
using Xamarin.Forms;

namespace CookingRecipes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var navigationPage = new NavigationPage();
            var recipesPage = new RecipesPage(new RecipesPageModel(new RecipesDataProvider(), navigationPage.Navigation));
            navigationPage.PushAsync(recipesPage);
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
