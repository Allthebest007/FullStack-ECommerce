using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.DTO
{
    public class Response<T> where T : class
    {
        public T Data { get; private set; }
        public int StatusCode { get; private set; }

        // We can use this propery only within backend side that's why we specify with JsonIgnore.
        [JsonIgnore]
        public bool IsSuccessfull { get; set; }
        public ErrorDTO Errors { get; set; }

        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T>
            {
                // We don't need to response data when we update or remove element. That's why " Data " is default value.
                Data = default,
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static Response<T> Fail(ErrorDTO errorDto, int statusCode)
        {
            return new Response<T>
            {
                Errors = errorDto,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }

        public static Response<T> Fail(string error, int statusCode,bool isShow)
        {
            return new Response<T>
            {
                Errors = new ErrorDTO(error,isShow),
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }

    }
}
