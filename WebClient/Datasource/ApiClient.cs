using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Datasource;

public class ApiClient : IApiClient
{
    protected readonly HttpClient Client;
    private readonly AppSettings _appSettings;
    private readonly Uri _baseAddress;

    public ApiClient(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        _baseAddress = new Uri(_appSettings.BaseUri + '/');
        Client = new HttpClient(CreateDefaultClientHandler());
        Client.DefaultRequestHeaders.ConnectionClose = true;
        Client.BaseAddress = _baseAddress;
    }

    private HttpClientHandler CreateDefaultClientHandler()
    {
        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = true,
            UseCookies = true,
            CookieContainer = new CookieContainer(),
            UseDefaultCredentials = true
        };
        return handler;
    }

    public async Task<TResponse?> GetAsync<TResponse>(string url)
    {
        HttpResponseMessage response = await Client.GetAsync(url);
        return await Deserialize<TResponse>(response);
    }

    public async Task<TResponse?> PostAsync<TResponse, TPayload>(string url, TPayload body)
    {
        var byteContent = ConvertToByteContent<TPayload>(body);
        HttpResponseMessage response = await Client.PostAsync(url, byteContent);
        return await Deserialize<TResponse>(response);
    }

    public async Task<HttpResponseMessage> RawPostAsync<TPayload>(string url, TPayload body)
    {
        var byteContent = ConvertToByteContent<TPayload>(body);
        HttpResponseMessage response = await Client.PostAsync(url, byteContent);
        return response;
    }

    public async Task<TResponse?> PutAsync<TResponse, TPayload>(string url, TPayload body)
    {
        var byteContent = ConvertToByteContent<TPayload>(body);
        HttpResponseMessage response = await Client.PutAsync(url, byteContent);
        return await Deserialize<TResponse>(response);
    }

    public async Task<TResponse?> DeleteAsync<TResponse>(string url)
    {
        HttpResponseMessage response = await Client.DeleteAsync(url);
        return await Deserialize<TResponse>(response);
    }

    private async Task<TResponse?> Deserialize<TResponse>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        string strData = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(strData))
        {
            return default(TResponse);
        }
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        var wrapResponse = System.Text.Json.JsonSerializer.Deserialize<WrapResponse<TResponse>>(strData, options);
        return wrapResponse.Result;
    }

    private ByteArrayContent ConvertToByteContent<TPayload>(TPayload body)
    {
        var content = JsonConvert.SerializeObject(body);
        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return byteContent;
    }
}
