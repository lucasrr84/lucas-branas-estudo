using account.src.domain.entity;

namespace account.src.infra.repository;

public interface IAccountRepository
{
    Task<Account?> getAccountByEmail(string email);
	Task<Account?> getAccountById(string accountId);
	Task saveAccount(Account account);
}
