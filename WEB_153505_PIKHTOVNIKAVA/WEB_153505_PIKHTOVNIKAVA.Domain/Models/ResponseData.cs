using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153505_PIKHTOVNIKAVA.Domain.Models
{
    public class ResponseData<T>
    {

        public ResponseData() { }
        public ResponseData(T data, string error_mess) 
        {
            this.Data = data;
            this.ErrorMessage = error_mess;
        }
        // запрашиваемые данные
        public T Data { get; set; }
        // признак успешного завершения запроса
        public bool Success { get; set; } = true;
        // сообщение в случае неуспешного завершения
        public string? ErrorMessage { get; set; }
    }
}
