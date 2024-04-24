namespace Blog.Web.Models;

public class Result
{
    public bool isSuccess { get; set; }
    public string message { get; set; } = default!;
}
public class Result<T> : Result
{
    public T Data { get; set; } = default!;
}
