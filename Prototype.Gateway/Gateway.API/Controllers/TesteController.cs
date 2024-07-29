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
        var transaction = await _processTransactions.GetAllTransaction();
        return Ok(transaction);
    }

    [HttpGet("ID")]
    public async Task<ActionResult> GetTransactionByID(string ID)
    {
        var transactions = await _processTransactions.GetTransactionByID(ID);
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<ActionResult<InternalResponse>> Get(TransactionDTO transaction)
    {
        var a = await _processTransactions.AddProcess(transaction, "client");
        return Ok(a);
    }
}
