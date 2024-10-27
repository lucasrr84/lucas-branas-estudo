using account.src.domain.dto;
using account.src.infra.repository;

namespace account.src.application.usecase;

public class GetAccount
{
    private readonly IAccountRepository _accountRepository;

    public GetAccount(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto> execute(string accountId)
    {
        var account = await _accountRepository.getAccountById(accountId);
        if (account == null) throw new Exception("Account not found");

        // DTO - Data Transfer Object
        return new AccountDto
        {
            accountId = account.getAccountId(),
            name = account.getName(),
            email = account.getEmail(),
            cpf = account.getCpf(),
            carPlate = account.getCarPlate(),
            password = account.getPassword()!,
            isPassenger = account.getIsPassenger(),
            isDriver = account.getIsDriver()
        }; 
    }
}