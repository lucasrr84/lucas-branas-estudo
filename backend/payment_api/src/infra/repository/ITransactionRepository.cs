using src.domain;

namespace src.infra.repository;

public interface ITransactionRepository
{
    void saveTransaction(Transaction transaction);
    Transaction getTransactionById(int transactionId);
}
