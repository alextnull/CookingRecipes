using CookingRecipes.Data;
using CookingRecipes.PageModels;
using CookingRecipes.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingRecipes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new RecipesPage(new RecipesPageModel(new RecipesDataProvider()));
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
