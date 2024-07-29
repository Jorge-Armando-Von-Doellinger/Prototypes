using Gateway.Application.DTOs;

namespace Gateway.Application.DataValidations;

public class TransactionValidation
{
    public async Task<bool> SimpleValidation(TransactionDTO transaction)
    {
        bool originValidation = string.IsNullOrEmpty(transaction.Origin) || 
            transaction.Origin.Trim().Length > 0;
        bool destationValidation = string.IsNullOrEmpty(transaction.Destination) ||
            transaction.Destination.Trim().Length > 0;
        bool dataValidation = string.IsNullOrEmpty(transaction.DataJson.ToString()) ||
            transaction.DataJson.ToString().Trim().Length > 4;
        bool transactionSucess = originValidation && destationValidation && dataValidation;
        return await Task.FromResult(transactionSucess);
    }
}
