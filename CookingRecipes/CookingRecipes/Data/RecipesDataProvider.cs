using CookingRecipes.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookingRecipes.Data
{
    /// <summary>Провайдер данных для рецептов.</summary>
    public class RecipesDataProvider : IRecipesDataProvider
    {
        /// <summary>Клиент для загрузки json.</summary>
        private HttpClient _httpClient;

        /// <summary>Заменяет html тэг <br> на перенос строки в инструкциях рецепта.</summary>
        /// <param name="recipes">Список рецептов</param>
        private void ReplaceTagsWithNewLine(List<Recipe> recipes)
        {
            recipes.ForEach(r => r.Instructions.Replace("<br>", Environment.NewLine));
        }

        /// <summary>Возвращает список рецептов.</summary>
        /// <returns>Список рецептов.</returns>
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var recipesJson = await _httpClient.GetStringAsync("https://test.kode-t.ru/recipes.json");
            JObject jsonObject = JObject.Parse(recipesJson);
            recipesJson = jsonObject["recipes"].ToString();
            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(recipesJson, new UnixDateTimeConverter());
            ReplaceTagsWithNewLine(recipes);
            return recipes;
        }

        /// <summary>Возвращает рецепт с указанным Id.</summary>
        /// <param name="uuid">Id рецепта.</param>
        /// <returns>Рецепт.</returns>
        public async Task<Recipe> GetRecipeAsync(Guid uuid)
        {
            var recipes = await GetRecipesAsync();
            var recipe = recipes.Single(r => r.Uuid == uuid);
            return recipe;
        }

        /// <summary>Инициализирует провайдер данных для рецептов.</summary>
        public RecipesDataProvider()
        {
            _httpClient = new HttpClient();
        }
    }
}
