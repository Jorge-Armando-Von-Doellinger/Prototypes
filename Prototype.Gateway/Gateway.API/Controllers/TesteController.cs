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
    public ActionResult<InternalResponse> Get([FromBody] TransactionDTO transaction)
    {
        var a = _processTransactions.AddProcess(transaction, "").ToJson();
        Console.WriteLine(a);
        return Ok(a);
    }
}
