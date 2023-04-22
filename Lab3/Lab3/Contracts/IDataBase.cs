using Lab3.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Contracts
{
    /// <summary>
    /// Засоби для роботи із базою даних.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataBase<T> where T : IIdentifiable
    {
        /// <summary>
        /// Метод для додавання нового елементу в базу даних.
        /// </summary>
        /// <param name="value">Елемент, який треба додати.</param>
        void Insert(T value);

        /// <summary>
        /// Метод що повертає елемент бази даних із вказаним guid.
        /// </summary>
        /// <param name="guid">Guid елементу, який потрібно знайти.</param>
        /// <returns>Елемент бази даних із вказаним guid, якщо такий присутній, інакше - null.</returns>
        T GetValueById(Guid guid);

        /// <summary>
        /// Метод що повертає колекцію всіх елементів бази даних.
        /// </summary>
        /// <returns>Колекція IEnumerable.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Метод для оновлення даних певного елементу бази даних.
        /// </summary>
        /// <param name="value">Елемент для оновлення.</param>
        void Update(T value);

        /// <summary>
        /// Метод для видалення елементу з бази даних.
        /// </summary>
        /// <param name="guid">Guid елементу для видалення.</param>
        /// <returns>True - якщо видалення пройшло успішно, інаше - false.</returns>
        bool Delete(Guid guid);
    }
}
