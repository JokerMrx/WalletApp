using Microsoft.AspNetCore.Mvc;
using WalletApp.Core.Models.DTOs;
using WalletApp.Core.Repositories;

namespace WalletApp.API.Controllers;

[ApiController]
[Route("/api/transactions")]
public class TransactionController : Controller
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ResponseDto _responseDto = new();

    public TransactionController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        try
        {
            
            
            return Ok(_responseDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _responseDto.Message = e.Message;
            _responseDto.IsSuccess = false;

            return BadRequest(_responseDto);
        }
    }
}