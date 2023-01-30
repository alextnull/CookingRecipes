using System;
using System.Collections.Generic;

namespace CookingRecipes.Models
{
    /// <summary>Модель рецепта.</summary>
    public class Recipe
    {
        /// <summary>Уникальный Id.</summary>
        public Guid Uuid { get; set; }

        /// <summary>Название.</summary>
        public string Name { get; set; }

        /// <summary>Изображения блюда.</summary>
        public List<string> Images { get; set; }

        /// <summary>Последняя дата обновления.</summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>Описание.</summary>
        public string Description { get; set; }

        /// <summary>Инструкции для приготовления блюда.</summary>
        public string Instructions { get; set; }

        /// <summary>Сложность.</summary>
        public int Difficulty { get; set; }
    }
}
