using CookingRecipes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookingRecipes.PageModels
{
    public class RecipeDetailPageModel : PageModel
    {
        /// <summary>Рецепт.</summary>
        private Recipe _recipe;

        /// <summary>Провайдер для навигации.</summary>
        private INavigation _navigationProvider;

        public Recipe Recipe
        {
            get => _recipe;
            set => SetProperty(ref _recipe, value);
        }

        /// <summary>Инициализирует PageModel деталей рецепта.</summary>
        /// <param name="recipe">Рецепт.</param>
        /// <param name="navigationProvider">Провайдер для навигации.</param>
        public RecipeDetailPageModel(Recipe recipe, INavigation navigationProvider)
        {
            _navigationProvider = navigationProvider;
            Recipe = recipe;
        }
    }
}
