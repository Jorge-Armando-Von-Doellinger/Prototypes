using System.Text.Json;
using Gateway.Application.DataValidations;
using Gateway.Application.DTOs;
using Gateway.Application.Map;
using Gateway.Core.Entity;

namespace Gateway.Application.Services;

public class DataService
{
    private readonly TransactionMapper _mapper;
    private readonly TransactionValidation _validation;
    public DataService(TransactionMapper mapper, TransactionValidation validation)
    {
         _mapper = mapper;
         _validation = validation;
    }



    public async Task<TransactionEntity> DataManipulation(TransactionDTO transactionDTO)
    {
        if(await _validation.SimpleValidation(transactionDTO) == false)
            throw new Exception("Dados inválidos!");
        
        //System.Console.WriteLine(transactionDTO.DataJson);
        return await Task.FromResult( _mapper.Map(transactionDTO) );
    }
}
