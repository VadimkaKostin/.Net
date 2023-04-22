using Lab3.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Contracts
{
    /// <summary>
    /// Засоби роботи із сервером.
    /// </summary>
    public interface IServer : IIdentifiable
    {
        /// <summary>
        /// Ім'я серверу.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Поточне навантаження сервера.
        /// </summary>
        int CurrentLoad { get; set; }

        /// <summary>
        /// Максимальне навантаження сервера.
        /// </summary>
        int MaxLoad { get; set; }

        /// <summary>
        /// Стан серверу.
        /// </summary>
        bool IsOnline { get; set; }

        /// <summary>
        /// Метод, що перевіряє, чи може зараз сервер отримати запит.
        /// </summary>
        /// <returns>True - якщо сервер може отримати запит, інакше - false.</returns>
        bool CanHandleRequest(int requestLoad);

        /// <summary>
        /// Метод для опрацювання запиту користувача.
        /// </summary>
        /// <param name="request">Request об'єкт - запит на сервер.</param>
        /// <returns>Responce об'єкт - відповідь сервера, null якщо всі сервера знаходятьтся в режимі offline.</returns>
        Task<Response> HandleRequest(Request request);
    }
}
