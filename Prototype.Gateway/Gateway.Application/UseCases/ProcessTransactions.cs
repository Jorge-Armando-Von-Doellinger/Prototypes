using System.ComponentModel.Design.Serialization;
using Amazon.Runtime.Internal;
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

    private const string OperationError = "Ocorreu um erro durante a operação";
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

    public async Task<InternalResponse> UpdateProcess(string transactionID, TransactionDTO data)
    {
        try
        {
            var transaction = await _dataService.DataManipulation(data, transactionID);
            if(await _repository.UpdateTransaction(transaction) == false)
                throw new Exception();
        }
        catch (Exception ex)
        {
            await SimpleCatcher(ex.Message);
        }
        return _response;
    }

    public async Task<InternalResponse> DeleteProcess(string transactionID)
    {
        try
        {
            if(await _repository.DeleteTransaction(transactionID) == false)
                throw new Exception(OperationError);
        }
        catch(Exception ex) 
        {
            await SimpleCatcher(ex.Message);
        }
        return _response;
    }

    public async Task<InternalResponse> GetAllTransaction()
    {
        try 
        {
            var data = await _repository.GetTransactions();
            await Task.Run(() => _response.ListDataJson = data);
        }
        catch (Exception ex)
        {
            await SimpleCatcher(ex.Message);
        }
        return _response;
    }

    public async Task<InternalResponse> GetTransactionByID(string transactionID)
    {
        try
        {
            var data = await _repository.GetTransactionByID(transactionID);
            if(data != null)
                _response.DataJson = data;
            throw new Exception(OperationError);
        }
        catch (Exception ex)
        {
            await SimpleCatcher(ex.Message);
        }
        return _response;
    }
}
