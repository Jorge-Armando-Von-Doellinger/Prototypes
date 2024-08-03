using System.Text.Json;
using Gateway.Application.DataValidations;
using Gateway.Application.DTOs;
using Gateway.Application.Map;
using Gateway.Core.Entity;

namespace Gateway.Application.Services;

public class DataService
{
    private readonly Mapper _mapper;
    private readonly TransactionValidation _validation;
    public DataService(Mapper mapper, TransactionValidation validation)
    {
         _mapper = mapper;
         _validation = validation;
    }



    public async Task<TransactionEntity> DataManipulation(TransactionDTO transactionDTO)
    {
        if(await _validation.SimpleValidation(transactionDTO) == false)
            throw new Exception("Dados inválidos!");
        
        return await Task.FromResult( _mapper.TransactionMapper.Map(transactionDTO) );
    }
    public async Task<MessageEntity> DataManipulation(TransactionEntity transaction)
    {
        return await _mapper.MessageMapper.MapClient(transaction);
    }
}
