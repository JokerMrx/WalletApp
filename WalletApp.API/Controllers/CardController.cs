using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WalletApp.API.Contracts.Card;
using WalletApp.BL.Utils;
using WalletApp.Core.Models;
using WalletApp.Core.Models.DTOs;
using WalletApp.Core.Repositories;

namespace WalletApp.API.Controllers;

[ApiController]
[Route("api/cards")]
public class CardController : Controller
{
    private readonly ICardRepository _cardRepository;
    private readonly ICardBalanceRandom _cardBalanceRandom;
    private readonly IUserRepository _userRepository;
    private readonly ResponseDto _responseDto = new();
    private readonly IMapper _mapper;

    public CardController(ICardRepository cardRepository, ICardBalanceRandom cardBalanceRandom,
        IUserRepository userRepository, IMapper mapper)
    {
        _cardRepository = cardRepository;
        _cardBalanceRandom = cardBalanceRandom;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCardRequest createCardRequest)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(createCardRequest.UserId);
            var cardBalance = _cardBalanceRandom.RndBalance();
            var card = new Card()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Balance = cardBalance,
                Available = (decimal)Core.Constants.Card.MaxBalance - cardBalance,
            };
            var createdCard = await _cardRepository.CreateAsync(card);
            _responseDto.Result = _mapper.Map<CardDto>(createdCard);

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
            var card = await _cardRepository.GetByIdAsync(id);
            _responseDto.Result = card;

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

    [HttpPatch("{id}/deposit")]
    public async Task<IActionResult> Deposit(Guid id, [FromBody] decimal amount)
    {
        try
        {
            var card = await _cardRepository.DepositFundsAsync(id, amount);
            _responseDto.Result = card;

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

    [HttpPatch("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id, [FromBody] decimal amount)
    {
        try
        {
            var card = await _cardRepository.WithdrawFundsAsync(id, amount);
            _responseDto.Result = card;

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