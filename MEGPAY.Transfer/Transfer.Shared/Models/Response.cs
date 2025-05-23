using System.Net;

namespace Transfer.Shared.Models;

public class Response<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Response<T> Success(T data, HttpStatusCode statusCode)
    {
        return new Response<T> { Data = data, StatusCode = statusCode };
    }

    public static Response<T> Success(T data, HttpStatusCode statusCode, string message)
    {
        return new Response<T> { Data = data, StatusCode = statusCode, Message = message };
    }

    public static Response<T> Fail(string? message, HttpStatusCode statusCode)
    {
        return new Response<T>()
        {
            Message = message,
            StatusCode = statusCode
        };
    }

    public static Response<T> Fail(List<string>? message, HttpStatusCode statusCode)
    {
        return new Response<T>()
        {
            Message = message == null ? null : string.Join(", ", message),
            StatusCode = statusCode
        };
    }

    public static Response<T> NotValid(T data, string? message = null)
    {
        return new Response<T>()
        {
            Data = data,
            Message = message,
            StatusCode = HttpStatusCode.UnprocessableEntity
        };
    }
}