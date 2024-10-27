using account.src.domain.entity;

namespace account.src.infra.repository;

public class AccountRepositoryMemory : IAccountRepository
{
    private readonly List<Account> accounts;

    public AccountRepositoryMemory()
    {
        accounts = new List<Account>();
    }

    public async Task<Account?> getAccountByEmail(string email)
    {
        return await Task.FromResult(accounts.FirstOrDefault(account => account.getEmail() == email));
    }

    public async Task<Account?> getAccountById(string accountId)
    {
        return await Task.FromResult(accounts.FirstOrDefault(account => account.getAccountId() == accountId));
    }

    public async Task saveAccount(Account account)
    {
        await Task.Run(() => accounts.Add(account));
    }
}