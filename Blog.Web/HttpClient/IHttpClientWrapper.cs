using Blog.Web.Models;

namespace Blog.Web.HttpClient;

public interface IHttpClientWrapper
{
    Task<Result<T>> GetAsync<T>(string url, string token, Dictionary<string, object> parameters);
    Task<Result<T>> PostAsync<T>(string url, string? token, string json);
    Task<Result> PutAsync<T>(string url, string token, string json);
    Task<Result> DeleteAsync(string url, string token);

}