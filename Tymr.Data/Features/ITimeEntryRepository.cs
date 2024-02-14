using CSharpFunctionalExtensions;
using Tymr.Data.Entities;

namespace Tymr.Data.Features
{
    public interface ITimeEntryRepository
    {
        Result<IEnumerable<TimeEntry>> GetAll();
        Task<Result> AddAsync(TimeEntryRequest entry);
        Task<Result> UpdateAsync(TimeEntryRequest entry);
        Task<Result> DeleteAsync(TimeEntry entry);
        Task<Result<IEnumerable<TimeEntry>>> GetByDateAsync(DateTime date);
    }
}
