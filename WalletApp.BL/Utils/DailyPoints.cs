namespace WalletApp.BL.Utils;

public class DailyPoints : IDailyPoints
{
    public int CalculateDailyPoints(int dayOfSeason)
    {
        int points = 0;

        if (dayOfSeason == 1)
        {
            points = 2;
        }
        else if (dayOfSeason == 2)
        {
            points = 3;
        }
        else
        {
            int previousDayPoints = 3;
            int dayBeforePreviousPoints = 2;

            for (int day = 3; day <= dayOfSeason; day++)
            {
                points = (int)(previousDayPoints * 0.6 + dayBeforePreviousPoints);
                dayBeforePreviousPoints = previousDayPoints;
                previousDayPoints = points;
            }
        }

        return points;
    }

    public string FormatDailyPoints(int dailyPoints)
    {
        if (dailyPoints >= 1000000)
        {
            return $"{dailyPoints / 1000000}M";
        }
        else if (dailyPoints >= 1000)
        {
            return $"{dailyPoints / 1000}K";
        }

        return dailyPoints.ToString();
    }
}