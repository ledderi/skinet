using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefualtMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefualtMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request you have made",
                401 => "Authorized you are not",
                404 => "resource found, it was not",
                500 => "Error are the path to the dark side. Errors lead to anger. anger leads to hate. hate leads to career change",
                _ => null
            };
        }
    }
}
