using System.Text.Json.Nodes;
using Gateway.Core.Entity;
using Gateway.Core.Responses;

namespace Gateway.Core.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        Task<List<JsonObject>> GetTransactions();
        Task<JsonObject> GetTransactionByID(string ID);
        Task<bool> AddTransaction(TransactionEntity transaction);
        Task<bool> UpdateTransaction(TransactionEntity transaction, string transactionID);
        Task<bool> DeleteTransaction(string ID);
    }
}


