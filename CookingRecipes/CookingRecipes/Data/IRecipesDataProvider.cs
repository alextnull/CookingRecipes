using CookingRecipes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingRecipes.Data
{
    /// <summary>Интерфейс для провайдеров данных.</summary>
    public interface IRecipesDataProvider
    {
        /// <summary>Возвращает список рецептов.</summary>
        /// <returns>Список рецептов.</returns>
        Task<Recipe> GetRecipeAsync(Guid uuid);

        /// <summary>Возвращает рецепт с указанным Id.</summary>
        /// <param name="uuid">Id рецепта.</param>
        /// <returns>Рецепт.</returns>
        Task<List<Recipe>> GetRecipesAsync();
    }
}