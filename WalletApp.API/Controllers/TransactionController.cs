using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletApp.API.Contracts.Transaction;
using WalletApp.Core.Enums;
using WalletApp.Core.Models;
using WalletApp.Core.Models.DTOs;
using WalletApp.Core.Repositories;

namespace WalletApp.API.Controllers;

[ApiController]
[Route("/api/transactions")]
public class TransactionController : Controller
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IUserRepository _userRepository;
    private readonly ResponseDto _responseDto = new();
    private readonly IMapper _mapper;

    public TransactionController(ITransactionRepository transactionRepository, ICardRepository cardRepository,
        IUserRepository userRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _cardRepository = cardRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionRequest createTransactionRequest)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(createTransactionRequest.AuthorizedUserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var card = await _cardRepository.GetByIdAsync(createTransactionRequest.CardId);

            if (card == null)
            {
                return NotFound("Card not found");
            }

            switch (createTransactionRequest.TransactionType)
            {
                case TransactionTypeEnum.Credit:
                {
                    await _cardRepository.WithdrawFundsAsync(card.Id, createTransactionRequest.Sum);
                    break;
                }
                case TransactionTypeEnum.Payment:
                {
                    await _cardRepository.DepositFundsAsync(card.Id, createTransactionRequest.Sum);
                    break;
                }
            }

            var transaction = new Transaction()
            {
                Id = new Guid(),
                Name = createTransactionRequest.Name,
                Description = createTransactionRequest.Description,
                CardId = createTransactionRequest.CardId,
                Sum = createTransactionRequest.Sum,
                TransactionType = createTransactionRequest.TransactionType,
                AuthorizedUserId = createTransactionRequest.AuthorizedUserId,
            };
            var createdTransaction = await _transactionRepository.CreateAsync(transaction);
            _responseDto.Result = _mapper.Map<TransactionDto>(createdTransaction);

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);

            return Ok(transactionDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _responseDto.Message = e.Message;
            _responseDto.IsSuccess = false;

            return BadRequest(_responseDto);
        }
    }

    [HttpGet("cards/{cardId}")]
    public async Task<IActionResult> GetAll(Guid cardId, [FromQuery] PageParams pageParams)
    {
        try
        {
            var transactions = await _transactionRepository.GetAllAsync(cardId, pageParams);
            _responseDto.Result = _mapper.Map<IEnumerable<TransactionDto>>(transactions);

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

    [HttpPatch("{id}/approve")]
    public async Task<IActionResult> Approve(Guid id, [FromBody] Guid userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            var approvedTransaction = await _transactionRepository.ApproveAsync(transaction.Id, user.Id);
            _responseDto.Result = _mapper.Map<TransactionDto>(approvedTransaction);

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

    [HttpPatch("{id}/reject")]
    public async Task<IActionResult> Reject(Guid id, [FromBody] Guid userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            var approvedTransaction = await _transactionRepository.RejectAsync(transaction.Id, user.Id);
            _responseDto.Result = _mapper.Map<TransactionDto>(approvedTransaction);

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