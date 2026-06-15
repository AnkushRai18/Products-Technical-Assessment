using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.APPLICATION.Services;

public class Result
{
    public bool IsSucceeded { get; set; }
    public List<string> Errors { get; set; } = [];

    public static Result Success()
    {
        return new Result { IsSucceeded = true };
    }

    public static Result Failure(params string[] errors)
    {
        return new Result
        {
            IsSucceeded = false,
            Errors = [.. errors]
        };
    }

    // ✅ Generic factory methods live HERE
    public static Result<T> IsSuccessed<T>(T data)
    {
        return new Result<T>
        {
            IsSucceeded = true,
            Data = data
        };
    }

    public static Result<T> Failure<T>(string error)
    {
        return new Result<T>
        {
            IsSucceeded = false,
            Error = error
        };
    }
}

public class Result<T>
{
    public bool IsSucceeded { get; set; }
    public string Error { get; set; } = string.Empty;
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public bool Success { get; set; }
    //public string Message { get; set; } = string.Empty;


    // Optional implicit conversion
    public static implicit operator Result<T>(Result v)
    {
        return new Result<T>
        {
            IsSucceeded = v.IsSucceeded,
            Error = v.Errors.FirstOrDefault() ?? string.Empty
        };
    }


}