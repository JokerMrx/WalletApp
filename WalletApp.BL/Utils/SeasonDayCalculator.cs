namespace WalletApp.BL.Utils;

public class SeasonDayCalculator
{
    public static int GetDayOfSeason(DateTime date)
    {
        DateTime seasonStart;
        
        if (date.Month >= 3 && date.Month <= 5)
        {
            seasonStart = new DateTime(date.Year, 3, 1);
        }
        else if (date.Month >= 6 && date.Month <= 8) 
        {
            seasonStart = new DateTime(date.Year, 6, 1);
        }
        else if (date.Month >= 9 && date.Month <= 11)
        {
            seasonStart = new DateTime(date.Year, 9, 1);
        }
        else
        {
            seasonStart = new DateTime(date.Month == 12 ? date.Year : date.Year - 1, 12, 1); // 1 грудня
        }
        
        return (date - seasonStart).Days + 1;
    }
}