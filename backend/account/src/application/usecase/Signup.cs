using account.src.domain.dto;
using account.src.domain.entity;
using account.src.infra.gateway;
using account.src.infra.repository;

namespace account.src.application.usecase;

public class Signup
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMailerGateway _mailerGateway;

    public Signup(IAccountRepository accountRepository, IMailerGateway mailerGateway)
    {
        _accountRepository = accountRepository;
        _mailerGateway = mailerGateway;
    }

    public async Task<SignupResponseDto> execute(AccountDto input)
    {
        //orquestrar entidades
        var account = Account.create(input.name, input.email, input.cpf, input.carPlate, input.password, input.isPassenger, input.isDriver, input.passwordType);

        //orquestar recursos
        var accountData = await _accountRepository.getAccountByEmail(input.email);
        if (accountData != null) throw new Exception("Duplicated account");
        await _accountRepository.saveAccount(account);
        await _mailerGateway.send(account.getEmail(), "Welcome!", "...");
        
        return new SignupResponseDto
        {
            accountId = account.getAccountId()
        };
    }
}