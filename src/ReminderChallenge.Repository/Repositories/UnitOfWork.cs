namespace ReminderChallenge.Repository.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ReminderContext _dbcontext;

    public UnitOfWork(ReminderContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbcontext.SaveChangesAsync(cancellationToken);
    }
}