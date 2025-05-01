using Microsoft.EntityFrameworkCore;
using ReminderChallenge.Repository.Entities;

namespace ReminderChallenge.Repository.Repositories;

public class ReminderRepository : IReminderRepository
{

    private readonly ReminderContext _dbContext;

    public ReminderRepository(ReminderContext dbContext) => 
        _dbContext = dbContext;

    public async Task<Reminder> AddSync(Reminder reminder)
    {
        await _dbContext.Reminder.AddAsync(reminder);
        return reminder;
    }

    public async Task<List<Reminder>> GetAllAsync()
    {
        var query = _dbContext.Reminder.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<Reminder> GetByIdAsync(Guid id)
    {
        var query = _dbContext.Reminder
            .Where(x => x.Id == id);

        var reminder = await query.FirstOrDefaultAsync();

        if (reminder is null)
        {
            throw new Exception($"No se encontro al recordatorio con el id: '{id}'");
        }

        return reminder;    
    }
}
