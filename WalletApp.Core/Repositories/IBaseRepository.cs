namespace WalletApp.Core.Repositories;

public interface IBaseRepository<T, K> where T : class where K : class
{
    public Task<K> CreateAsync(T entity);
    public Task<K> GetByIdAsync(Guid id);
}