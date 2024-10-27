using System.Net.Http.Json;
using account.src.domain.dto;
using NUnit.Framework;

namespace test.tests.account
{
    public class apitest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:3001/account") };
        }

        //Deve criar a conta de um passageiro
        [Test]
        public async Task Deve_Criar_A_Conta_De_Um_Passageiro()
        {
            // Arrange
            var input = new SignupInputDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };
            
            // Act - Criar a conta
            var responseSignup = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/signup", input);
            responseSignup.EnsureSuccessStatusCode();
            var outputSignup = await responseSignup.Content.ReadFromJsonAsync<SignupResponseDto>();
            
            Assert.IsNotNull(outputSignup?.accountId);

            // Pega a conta criada
            var responseGetAccount = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/accounts/{outputSignup.accountId}");
            responseGetAccount.EnsureSuccessStatusCode();

            var outputGetAccount = await responseGetAccount.Content.ReadFromJsonAsync<AccountDto>();

            // Assert
            Assert.That(outputSignup.accountId, Is.EqualTo(outputGetAccount?.accountId));
            Assert.That(input.name, Is.EqualTo(outputGetAccount?.name));
            Assert.That(input.email, Is.EqualTo(outputGetAccount?.email));
            Assert.That(input.cpf, Is.EqualTo(outputGetAccount?.cpf));
            Assert.That(input.password, Is.EqualTo(outputGetAccount?.password));
            Assert.That(input.isPassenger, Is.EqualTo(outputGetAccount?.isPassenger));
        }

        //Não deve criar a conta de um passageiro
        [Test]
        public async Task Nao_Deve_Criar_A_Conta_De_Um_Passageiro()
        {
            // Arrange
            var input = new SignupInputDto
            {
                name = "John",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };
            
            // Act
            var responseSignup = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/signup", input);

            // Assert
            Assert.That((int)responseSignup.StatusCode, Is.EqualTo(422));
    
            var content = await responseSignup.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("Invalid name"));
        }
    }
}