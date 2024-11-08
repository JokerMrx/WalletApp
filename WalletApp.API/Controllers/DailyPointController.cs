using Microsoft.AspNetCore.Mvc;
using WalletApp.BL.Utils;
using WalletApp.Core.Models.DTOs;

namespace WalletApp.API.Controllers;

[ApiController]
[Route("api/daily-points")]
public class DailyPointController : Controller
{
    private readonly IDailyPoints _dailyPoints;
    private readonly ResponseDto _responseDto = new();

    public DailyPointController(IDailyPoints dailyPoints)
    {
        _dailyPoints = dailyPoints;
    }

    [HttpGet]
    public IActionResult GetDailyPoint()
    {
        try
        {
            var dayOfSeason = SeasonDayCalculator.GetDayOfSeason(DateTime.Now);
            var dailyPoints = _dailyPoints.CalculateDailyPoints(dayOfSeason);
            _responseDto.Result = _dailyPoints.FormatDailyPoints(dailyPoints);

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