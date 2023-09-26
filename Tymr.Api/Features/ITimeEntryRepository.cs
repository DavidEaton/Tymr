using CSharpFunctionalExtensions;
using Tymr.Data.Entities;

namespace Tymr.Api.Features
{
    public interface ITimeEntryRepository
    {

        Task<Result<IEnumerable<TimeEntry>>> GetAllAsync();
        Task<Result> AddAsync(TimeEntry entry);
        Task<Result> UpdateAsync(TimeEntry entry);
        Task<Result> DeleteAsync(int id);
        Task<Result<IEnumerable<TimeEntry>>> GetByDateAsync(DateTime date);
    }
}
