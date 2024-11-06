using WalletApp.Core.Models;

namespace WalletApp.Core.Repositories;

public interface IDailyPointRepository : IBaseRepository<DailyPoint, DailyPoint>
{
    public Task<int> CalculateForTodayAsync();
}