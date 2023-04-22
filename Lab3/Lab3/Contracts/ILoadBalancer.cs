using Lab3.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Contracts
{
    /// <summary>
    /// Набір інструментів для балансування навантаження серверів.
    /// </summary>
    public interface ILoadBalancer
    {
        /// <summary>
        /// Метод для додавання нового серверу в базу даних для балансування.
        /// </summary>
        /// <param name="server">IServer об'єкт для додавання.</param>
        void AddServer(IServer server);

        /// <summary>
        /// Метод для видалення серверу з бази даних для балансування.
        /// </summary>
        /// <param name="server">IServer об'єкт для видалення.</param>
        void RemoveServer(IServer server);

        /// <summary>
        /// Метод балансування що знаходить найменш навантажений сервер серед усіх 
        /// в базі даних для балансування.
        /// </summary>
        /// <returns>IServer об'єкт - сервер із найменшою завантаженістю, 
        /// null - якщо всі сервери знаходяться в режимі offline.</returns>
        IServer GetOptimalServer(int load);

        /// <summary>
        /// Метод для опрацювання запиту користувача, який буде виконувати балансування 
        /// та відправляти запит на найменш завантажений сервер.
        /// </summary>
        /// <param name="request">Request об'єкт - запит на сервер.</param>
        /// <returns>Responce об'єкт - відповідь сервера, null якщо всі сервера знаходятьтся в режимі offline.</returns>
        Task<Response> HandleRequest(Request request);
    }
}
