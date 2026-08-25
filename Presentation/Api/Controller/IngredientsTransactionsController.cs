using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Products;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class IngredientsTransactionsController : ControllerBase
{
    private readonly ProductTransactionServices _transactions;

    public IngredientsTransactionsController(ProductTransactionServices transactions)
    {
        _transactions = transactions;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetTransactionsAsync()
    {
        var transactionLists = await _transactions.GetInventoryTransactionAsync();

        return Ok(new Response<List<InventoryTransactionDTO>>
        {
            Success = true,
            Message = "Transaction List fetched",
            Data = transactionLists
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(Guid transactionID)
    {
        var transaction = await _transactions.GetTransactionById(transactionID);

        return Ok(new Response<InventoryTransactionDTO>
        {
            Success = true,
            Message = "Transaction Fetched",
            Data = transaction
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddTransaction(AddInventoryTransaction addInventoryTransaction, Guid ingredientID)
    {
        await _transactions.AddInventoryTransactions(addInventoryTransaction, ingredientID);

        return Ok(new Response<AddInventoryTransaction>
        {
            Success = true,
            Message = "Transaction added",
            Data = addInventoryTransaction
        });
    }
}