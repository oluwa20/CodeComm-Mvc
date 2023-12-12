using CodeComm.Models.ViewModel;
using Newtonsoft.Json;

public class AuthApiService
{
    private readonly HttpClient _httpClient;

    public AuthApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://dejidabiri-001-site1.atempurl.com")
        };
    }

    public async Task<bool> AuthenticateAsync(string usernameOrEmail, string userPassword)
    {
        var data = new
        {
            UsernameOrEmail = usernameOrEmail,
            UserPassword = userPassword
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/UserLogin", data);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RegisterAsync(CreateUserDto model)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/User/CreateUser", model);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            // Log or handle the specific exception
            return false;
        }
    }

    public async Task<bool> CreateChatAsync(CreateChat chatModel)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/CreateChat", chatModel);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            // Log or handle the specific exception
            return false;
        }
    }


}
