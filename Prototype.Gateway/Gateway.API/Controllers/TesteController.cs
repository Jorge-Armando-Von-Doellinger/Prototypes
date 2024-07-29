using Gateway.Application.DTOs;
using Gateway.Application.UseCases;
using Gateway.Core.Entity;
using Gateway.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Gateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TesteController : ControllerBase
{
    private readonly ProcessTransactions _processTransactions;
    public TesteController(ProcessTransactions processTransactions)
    {
        _processTransactions = processTransactions;
    }

    [HttpGet]
    public async Task<ActionResult> GetTransactions()
    {
        var result = await _processTransactions.GetAllTransaction();
        return Ok(result);
    }

    [HttpGet("ID")]
    public async Task<ActionResult> GetTransactionByID(string ID)
    {
        var result = await _processTransactions.GetTransactionByID(ID);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<InternalResponse>> AddTransaction(TransactionDTO transaction)
    {
        var result = await _processTransactions.AddProcess(transaction, "client");
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTransaction(TransactionDTO transaction, string transactionID)
    {
        var result = _processTransactions.UpdateProcess(transactionID, transaction);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTransaction(string transactionID)
    {
        var result = await _processTransactions.DeleteProcess(transactionID);
        return Ok(result);
    }
}
