using CSharpFunctionalExtensions;
using System.Text.Json;
using Tymr.Data.Entities;

namespace Tymr.Api.Features
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private const string FilePath = @"C:\Users\david\OneDrive\Janco\Tymr\Entries\time_entries.json";

        public async Task<Result> AddAsync(TimeEntry entry)
        {
            var entriesResult = await GetAllAsync();

            if (entriesResult.IsFailure)
                return Result.Failure("Failed to retrieve existing entries.");

            var entries = entriesResult.Value;

            if (entries.Any(e => e.Date == entry.Date && e.Begin == entry.Begin && e.End == entry.End))
                return Result.Failure("Entry with the same Date, Begin, and End time already exists.");

            entries = entries.Append(entry).ToArray();

            return await WriteAllAsync(entries);
        }

        public async Task<Result> DeleteAsync(TimeEntry entryToDelete)
        {
            var entriesResult = await GetAllAsync();

            if (entriesResult.IsFailure)
                return Result.Failure("Failed to retrieve existing entries.");

            var entries = entriesResult.Value;

            entries = entries
                        .Where(e => !(e.Date == entryToDelete.Date && e.Begin == entryToDelete.Begin && e.End == entryToDelete.End))
                        .ToArray();

            return await WriteAllAsync(entries);
        }

        public Task<Result> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<TimeEntry>>> GetAllAsync()
        {
            if (!File.Exists(FilePath))
                return Result.Success(new List<TimeEntry>() as IEnumerable<TimeEntry>);

            try
            {
                var json = await File.ReadAllTextAsync(FilePath);
                var entries = JsonSerializer.Deserialize<IEnumerable<TimeEntry>>(json);
                return Result.Success(entries);
            }
            catch
            {
                return Result.Failure<IEnumerable<TimeEntry>>("Failed to read from the file.");
            }
        }

        public async Task<Result<IEnumerable<TimeEntry>>> GetByDateAsync(DateTime date)
        {
            var allEntriesResult = await GetAllAsync();

            if (allEntriesResult.IsFailure)
                return Result.Failure<IEnumerable<TimeEntry>>(allEntriesResult.Error);

            return Result.Success(allEntriesResult.Value.Where(e => e.Date.Date == date.Date));
        }

        public async Task<Result> UpdateAsync(TimeEntry entry)
        {
            var entriesResult = await GetAllAsync();

            if (entriesResult.IsFailure)
                return Result.Failure("Failed to retrieve existing entries.");

            var entries = entriesResult.Value;

            if (!entries.Any(e => e.Date == entry.Date && e.Begin == entry.Begin && e.End == entry.End))
                return Result.Failure("Entry not found to update.");

            entries = entries
                        .Where(e => !(e.Date == entry.Date && e.Begin == entry.Begin && e.End == entry.End))
                        .Append(entry)
                        .ToArray();

            return await WriteAllAsync(entries);
        }

        private async Task<Result> WriteAllAsync(IEnumerable<TimeEntry> entries)
        {
            try
            {
                var json = JsonSerializer.Serialize(entries);
                await File.WriteAllTextAsync(FilePath, json);
                return Result.Success();
            }
            catch
            {
                return Result.Failure("Failed to write to the file.");
            }
        }
    }
}