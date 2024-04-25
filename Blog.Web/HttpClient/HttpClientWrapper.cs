using Blog.Web.Models;
using RestSharp;

namespace Blog.Web.HttpClient;

public class HTTPClientWrapper : IHttpClientWrapper
{
    private readonly IConfiguration _configuration;
    public HTTPClientWrapper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Result<T>> GetAsync<T>(string path, string token, Dictionary<string, object> parameters)
    {
        string apiUrl = _configuration.GetConnectionString("ApiUrl") ?? "";

        Result<T> result = new Result<T>();
        using (var httpClient = new RestClient($"{apiUrl}"))
        {
            var request = new RestRequest($"/{path}");
            request.Method = Method.Get;
            request.AddHeader("Authorization", "Bearer " + token);
            if (parameters != null)
            {
                if (parameters.Count > 0)
                {
                    foreach (var item in parameters)
                    {
                        if (item.Value != null)
                        {
                            if (item.Value.ToString()!.Contains(","))
                            {
                                for (int i = 0; i < item.Value!.ToString()!.Split(',').Count(); i++)
                                {
                                    request.AddQueryParameter(item.Key, item.Value == null ? "" : item.Value.ToString()!.Split(',')[i]);
                                }
                            }
                            else
                                request.AddQueryParameter(item.Key, item.Value == null ? "" : item.Value.ToString());
                        }

                        else
                            request.AddQueryParameter(item.Key, item.Value == null ? "" : item.Value.ToString());
                    }
                }
            }

            var response = await httpClient.ExecuteGetAsync<Result<T>>(request);
            if (response.Data != null)
            {
                result = response.Data;
            }

        }

        return result;
    }

    public async Task<Result<T>> PostAsync<T>(string path, string? token, string json)
    {
        string apiUrl = _configuration.GetConnectionString("ApiUrl") ?? "";

        Result<T> result = new Result<T>();
        using (var httpClient = new RestClient($"{apiUrl}"))
        {
            var request = new RestRequest($"/{path}", Method.Post);
            request.AddHeader("Accept", "application/json");

            if(!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", "Bearer " + token);


            request.Timeout = 30000;
            if (!string.IsNullOrEmpty(json))
            {
                request.AddParameter("application/json", json, ParameterType.RequestBody);
            }
            request.RequestFormat = DataFormat.Json;

            var response = await httpClient.ExecutePostAsync<Result<T>>(request);
            if (response.Data != null)
            {
                if (response.Data.isSuccess == false)
                {
                    result.isSuccess = response.Data.isSuccess;
                    result.message = response.Data.message;
                }
                else
                {
                    result = response.Data;
                }
            }
        }

        return result;
    }

    public async Task<Result> PutAsync<T>(string path, string token, string json)
    {
        string apiUrl = apiUrl = _configuration.GetConnectionString("ApiUrl") ?? ""; ;
     
        Result<T> result = new Result<T>();
        using (var httpClient = new RestClient($"{apiUrl}"))
        {
            var request = new RestRequest($"/{path}", Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            request.Timeout = 30000;
            if (!string.IsNullOrEmpty(json))
            {
                request.AddParameter("application/json", json, ParameterType.RequestBody);
            }
            request.RequestFormat = DataFormat.Json;

            var response = await httpClient.ExecutePutAsync<Result<T>>(request);

            if (response.Data != null)
            {
                if (response.Data.isSuccess == false)
                {
                    result.isSuccess = response.Data.isSuccess;
                    result.message = response.Data.message;
                }
                else
                {
                    result = response.Data;
                }
            }
        }

        return result;
    }

    public async Task<Result> DeleteAsync(string url, string token)
    {
        string apiUrl = _configuration.GetConnectionString("ApiUrl") ?? "";

        Result result = new Result();
        using (var httpClient = new RestClient($"{apiUrl}"))
        {
            var request = new RestRequest($"/{url}", Method.Delete);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            request.Timeout = 30000;

            var response = await httpClient.ExecuteAsync<Result>(request);
            if (response.Data != null)
            {
                if (response.Data.isSuccess == false)
                {
                    result.isSuccess = response.Data.isSuccess;
                    result.message = response.Data.message;
                }
                else
                {
                    result = response.Data;
                }
            }
        }

        return result;
    }

}
