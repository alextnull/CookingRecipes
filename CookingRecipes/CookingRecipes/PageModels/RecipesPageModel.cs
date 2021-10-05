using CookingRecipes.Data;
using CookingRecipes.Models;
using CookingRecipes.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingRecipes.PageModels
{
    /// <summary>PageModel рецептов.</summary>
    public class RecipesPageModel : PageModel
    {
        /// <summary>Провайдер данных для рецептов.</summary>
        private IRecipesDataProvider _recipesDataProvider;

        /// <summary>Провайдер для навигации.</summary>
        private INavigation _navigationProvider;

        /// <summary>Список рецептов.</summary>
        private List<Recipe> _recipes;

        /// <summary>Выбранный рецепт.</summary>
        private Recipe _selectedRecipe;

        /// <summary>Указывает, загружаются ли рецепты.</summary>
        private bool _isLoading;

        /// <summary>Выбранный способ сортировки.</summary>
        private string _selectedSortOption;

        /// <summary>Доступные способы сортировки.</summary>
        private List<string> _sortOptions;

        /// <summary>Доступные способы поиска.</summary>
        private List<string> _searchOptions;

        /// <summary>Выбранный способ поиска.</summary>
        private string _selectedSearchOption;

        /// <summary>Текст для поиска.</summary>
        private string _searchText = "";

        /// <summary>Список рецептов.</summary>
        public List<Recipe> Recipes
        {
            get => _recipes;
            set => SetProperty(ref _recipes, value);
        }

        /// <summary>Указывает, загружаются ли рецепты.</summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        /// <summary>Доступные способы сортировки.</summary>
        public List<string> SortOptions
        {
            get { return _sortOptions; }
            set { _sortOptions = value; }
        }

        /// <summary>Выбранный способ сортировки.</summary>
        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set => SetProperty(ref _selectedSortOption, value);
        }

        /// <summary>Доступные способы поиска.</summary>
        public List<string> SearchOptions
        {
            get { return _searchOptions; }
            set { _searchOptions = value; }
        }

        /// <summary>Выбранный способ поиска.</summary>
        public string SelectedSearchOption
        {
            get => _selectedSearchOption;
            set => SetProperty(ref _selectedSearchOption, value);
        }

        /// <summary>Текст для поиска.</summary>
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        /// <summary>Выбранный рецепт.</summary>
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set => SetProperty(ref _selectedRecipe, value);
        }

        /// <summary>Ищет рецепты.</summary>
        /// <param name="recipes">Список рецептов.</param>
        /// <returns>Найденные рецепты, либо исходный список, если в поиске нет необходимости.</returns>
        private List<Recipe> SearchRecipes(List<Recipe> recipes)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                return recipes;
            }
            switch (SelectedSearchOption)
            {
                case "Name":
                    return recipes.Where(r => r.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                case "Description":
                    return recipes.Where(r => r.Description.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                case "Instructions":
                    return recipes.Where(r => r.Instructions.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                default:
                    return recipes;
            }
        }

        /// <summary>Сортирует список рецептов.</summary>
        /// <param name="recipes">Список рецептов.</param>
        /// <returns>Отсортированный список рецептов, либо первоначальный, если не удалось отсортировать.</returns>
        private List<Recipe> SortRecipes(List<Recipe> recipes)
        {
            switch (SelectedSortOption)
            {
                case "Name":
                    return recipes.OrderBy(r => r.Name).ToList();
                case "Date":
                    return recipes.OrderByDescending(r => r.LastUpdated).ToList();
                default:
                    return recipes;
            }
        }

        /// <summary>Загружает рецепты.</summary>
        public async Task LoadRecipesAsync()
        {
            IsLoading = true;
            var recipes = await _recipesDataProvider.GetRecipesAsync();
            recipes = SearchRecipes(recipes);
            Recipes = SortRecipes(recipes);
            IsLoading = false;
        }

        /// <summary>Переходит на страницу с деталями рецепта.</summary>
        public async Task NavigateToRecipeDetailAsync()
        {
            var selectedRecipe = SelectedRecipe;
            SelectedRecipe = null;
            await _navigationProvider.PushAsync(new RecipeDetailPage(new RecipeDetailPageModel(selectedRecipe, _navigationProvider)));
        }

        /// <summary>Загружает рецепты.</summary>
        public ICommand LoadRecipesCommandAsync { get; set; }

        /// <summary>Переходит на страницу с деталями рецепта.</summary>
        public ICommand NavigateToRecipeDetailCommandAsync { get; set; }

        /// <summary>Инициализирует провайдеры.</summary>
        /// <param name="recipesDataProvider">Провайдер данных для рецептов.</param>
        /// <param name="navigationProvider">Провайдер для навигации.</param>
        private void InitializeProviders(IRecipesDataProvider recipesDataProvider, INavigation navigationProvider)
        {
            _recipesDataProvider = recipesDataProvider;
            _navigationProvider = navigationProvider;
        }

        /// <summary>Инициализирует опции для сортировки и поиска.</summary>
        private void InitializeSortAndSearchOptions()
        {
            SortOptions = new List<string>() { "Name", "Date" };
            SelectedSortOption = SortOptions[0];
            SearchOptions = new List<string>() { "Name", "Description", "Instructions" };
            SelectedSearchOption = SearchOptions[0];
        }

        /// <summary>Инициализирует команды.</summary>
        private void InitializeCommands()
        {
            LoadRecipesCommandAsync = new Command(async () => await LoadRecipesAsync());
            LoadRecipesCommandAsync.Execute(null);
            NavigateToRecipeDetailCommandAsync = new Command(async () => await NavigateToRecipeDetailAsync());
        }

        /// <summary>Инициализирует PageModel рецептов.</summary>
        /// <param name="recipesDataProvider"></param>
        public RecipesPageModel(IRecipesDataProvider recipesDataProvider, INavigation navigationProvider)
        {
            InitializeProviders(recipesDataProvider, navigationProvider);
            InitializeSortAndSearchOptions();
            InitializeCommands();
        }
    }
}
