namespace Blog.Application.Common.Models;

public class Result
{
    internal Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }


    public static Result Success()
    {
        return new Result(true, ResultMessages.Success);
    }

    public static Result Success(string Message = ResultMessages.Success)
    {
        return new Result(true, Message);
    }

    public static Result Failure(string Message = ResultMessages.Error)
    {
        return new Result(false, Message);
    }
}

public class Result<T> : Result
{
    internal Result(T data, bool isSuccess, string message) : base(isSuccess, message)
    {
        Data = data;
    }

    public T Data { get; set; }

    public static Result<T> Success(T data, string Message = ResultMessages.Success)
    {
        return new Result<T>(data, true, Message);
    }

    public static Result<T> Failure(string Message = ResultMessages.Error)
    {
        return new Result<T>(default, false, Message);
    }
}

