using System.Collections.Generic;
using prmToolkit.NotificationPattern;

namespace CloudMe.ToDeTaxi.Api.Models
{
    public class Response<T>
    {
        /// <summary> 
        /// Indica se a operação foi bem sucedida
        /// </summary> 
        public bool success { get; set; }

        /// <summary> 
        /// Notificações do domínio. 
        /// </summary> 
        public IEnumerable<Notification> notifications { get; set; }

        /// <summary> 
        /// Dados vinculados à resposta da operação (de qualquer formato)
        /// </summary> 
        public T data;

        /// <summary> 
        /// Utilizado para paginação de resultados
        /// </summary> 
        public int count;
    }
}
