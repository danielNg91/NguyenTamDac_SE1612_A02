using System.Net;

namespace WebClient.Datasource;

public interface IApiClient
{
    //void AddCokie(string cookieHeader);
    Task<TResponse?> GetAsync<TResponse>(string url);
    Task<TResponse?> PostAsync<TResponse, TPayload>(string url, TPayload body);
    Task<HttpResponseMessage> RawPostAsync<TPayload>(string url, TPayload body);
    Task<TResponse?> PutAsync<TResponse, TPayload>(string url, TPayload body);
    Task<TResponse?> DeleteAsync<TResponse>(string url);
}
