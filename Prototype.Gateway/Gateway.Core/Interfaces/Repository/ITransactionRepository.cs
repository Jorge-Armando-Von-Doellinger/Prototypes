using Gateway.Core.Entity;
using Gateway.Core.Responses;

namespace Gateway.Core.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        Task<List<TransactionEntity>> GetTransactions();
        Task<TransactionEntity> GetTransactionByID(string ID);
        Task<bool> AddTransaction(TransactionEntity transaction);
        Task<bool> UpdateTransaction(TransactionEntity transaction);
        Task<bool> DeleteTransaction(string ID);
    }
}


