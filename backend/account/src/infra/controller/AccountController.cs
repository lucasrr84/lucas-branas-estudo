using account.src.application.usecase;
using account.src.domain.dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace account.src.infra.controller
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly Signup _signup;
        private readonly GetAccount _getAccount;

        public AccountController(Signup signup, GetAccount getAccount)
        {
            _signup = signup;
            _getAccount = getAccount;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> signup([FromBody] SignupInputDto input)
        {
            try 
            {
                //Console.WriteLine($"Received input at signup: {JsonConvert.SerializeObject(input)}"); // Adicione esta linha para debug

                var accountDto = new AccountDto
                {
                    name = input.name,
                    email = input.email,
                    cpf = input.cpf,
                    password = input.password,
                    carPlate = input.carPlate,
                    isPassenger = input.isPassenger,
                    isDriver = input.isDriver
                };
                
                var output = await _signup.execute(accountDto);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(422, ex.Message);
            }
        }

        [HttpGet("accounts/{accountId}")]
        public async Task<IActionResult> getAccountById(string accountId)
        {
            try
            {
                //Console.WriteLine($"getAccountById!!!!! accountId = {accountId}");
                var output = await _getAccount.execute(accountId);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(422, ex.Message);
            }
        } 
    }
}