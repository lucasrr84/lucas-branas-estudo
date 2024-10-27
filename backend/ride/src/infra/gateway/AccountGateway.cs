using account.src.domain.dto;

namespace ride.src.infra.gateway;

public class AccountGateway
{
    private readonly HttpClient _httpClient;

    public AccountGateway(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<SignupResponseDto?> signup(SignupInputDto input)
    {
        var response = await _httpClient.PostAsJsonAsync($"http://localhost:3001/account/signup", input);
        return await response.Content.ReadFromJsonAsync<SignupResponseDto>();
    }

    public async Task<AccountDto?> getAccountById(string accountId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:3001/account/accounts/{accountId}");
        return await response.Content.ReadFromJsonAsync<AccountDto>();
    }
}