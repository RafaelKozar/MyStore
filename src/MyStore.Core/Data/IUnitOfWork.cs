namespace MyStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
    
}
