using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CookingRecipes.PageModels
{
    /// <summary>Базовый класс для PageModels.</summary>
    public class PageModel : INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Уведомляет об изменении свойства.</summary>
        /// <param name="propertyName">Имя свойства.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Устанавливает новое значение свойству.</summary>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <param name="field">Ссылка на поле, которое скрывает свойство.</param>
        /// <param name="value">Новое значение.</param>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns><see langword="true"/>, если значение свойства поменялось; иначе <see langword="false"/>.</returns>
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field != null && Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
