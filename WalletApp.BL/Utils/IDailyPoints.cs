namespace WalletApp.BL.Utils;

public interface IDailyPoints
{
    public int CalculateDailyPoints(int dayOfSeason);
    public string FormatDailyPoints(int dailyPoints);
}