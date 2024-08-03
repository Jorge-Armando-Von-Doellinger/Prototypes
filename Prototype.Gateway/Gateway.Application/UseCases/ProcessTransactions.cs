using System.ComponentModel.Design.Serialization;
using System.Text.Json.Nodes;
using Amazon.Runtime.Internal;
using Gateway.Application.DataValidations;
using Gateway.Application.DTOs;
using Gateway.Application.Map;
using Gateway.Application.Services;
using Gateway.Core.Entity;
using Gateway.Core.Interfaces.Repository;
using Gateway.Core.Responses;

namespace Gateway.Application.UseCases;

public class ProcessTransactions
{
    private readonly ITransactionRepository _repository;
    private readonly DataService _dataService;
    private readonly MessageService _messageService;
    private InternalResponse _response;

    private const string OperationError = "Ocorreu um erro durante a operação";
    public ProcessTransactions(ITransactionRepository repository, DataService dataService, MessageService messageService ,InternalResponse response)
    {
        _repository = repository;
        _dataService = dataService;
        _response = response;
        _messageService = messageService;
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
            TransactionEntity transaction = await _dataService.DataManipulation(data);
            MessageEntity message = await _dataService.DataManipulation(transaction);
            message.RoutingKey = routingKey;
            bool success = await _repository.AddTransaction(transaction);
            if (!success)
                throw new Exception("Houve um erro durante a criação da transação, tente novamente!");
            await  _messageService.PublishMessage(message);
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
            var transaction = await _dataService.DataManipulation(data);
            Console.WriteLine(transaction.DataJson.ToString());
            if(await _repository.UpdateTransaction(transaction, transactionID) == false)
                throw new Exception(OperationError);
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
            List<JsonObject> data = await _repository.GetTransactions();
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
            JsonObject data = await _repository.GetTransactionByID(transactionID);
            if(data == null)
                throw new Exception(OperationError);
            _response.DataJson = data;
        }
        catch (Exception ex)
        {
            await SimpleCatcher(ex.Message);
        }
        return _response;
    }
}
