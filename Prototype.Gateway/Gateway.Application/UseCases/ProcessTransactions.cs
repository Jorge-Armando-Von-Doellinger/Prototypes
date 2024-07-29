using System.ComponentModel.Design.Serialization;
using Gateway.Application.DataValidations;
using Gateway.Application.DTOs;
using Gateway.Application.Map;
using Gateway.Application.Services;
using Gateway.Core.Interfaces.Repository;
using Gateway.Core.Responses;

namespace Gateway.Application.UseCases;

public class ProcessTransactions
{
    private readonly ITransactionRepository _repository;
    private readonly DataService _dataService;
    private InternalResponse _response;
    public ProcessTransactions(ITransactionRepository repository, DataService dataService, InternalResponse response)
    {
          _repository = repository;
          _dataService = dataService;
          _response = response;
    }

    private async Task SimpleCatcher(string message)
    {
        _response.DataJson = null;
        _response.IsSuccess = false;
        _response.Message = message; 
        await Task.CompletedTask;
    }

    public async Task<InternalResponse> AddProcess(TransactionDTO data, string routingKey)
    {
        try
        {
            var transaction = await _dataService.DataManipulation(data);
            bool success = await _repository.AddTransaction(transaction);
            if (!success)
                throw new Exception("Houve um erro durante a criação da transação, tente novamente!");
        }
        catch (Exception ex)
        {
            await SimpleCatcher(ex.Message);
        }
        return _response;
    }
}
