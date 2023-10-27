using CSharpFunctionalExtensions;
using System.Text.Json;
using Tymr.Data.Entities;

namespace Tymr.Data.Features
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private const string FilePath = "Data/time_entries.json";

        public async Task<Result> AddAsync(TimeEntryRequest entryRequest)
        {
            var entriesResult = await GetAllAsync();

            if (entriesResult.IsFailure)
                return Result.Failure("Failed to retrieve existing entries.");
            var entries = entriesResult.Value;

            if (entryRequest.IsNotUnique(entries))
                return Result.Failure("Entry with the same Date, Begin, and End time already exists.");

            var entryResult = CreateTimeEntry(entryRequest);
            if (entryResult.IsFailure)
                return entryResult;

            return await SaveNewEntryAsync(entries, entryResult.Value);
        }

        private static Result<TimeEntry> CreateTimeEntry(TimeEntryRequest request)
        {
            return TimeEntry.Create(request.Begin, request.End);
        }

        private async Task<Result> SaveNewEntryAsync(IEnumerable<TimeEntry> existingEntries, TimeEntry newEntry)
        {
            var updatedEntries = existingEntries.Append(newEntry).ToArray();
            return await WriteAllAsync(updatedEntries);
        }

        public async Task<Result> DeleteAsync(TimeEntry entryToDelete)
        {
            var entriesResult = await GetAllAsync();

            if (entriesResult.IsFailure)
                return Result.Failure("Failed to retrieve existing entries.");

            var entries = entriesResult.Value;

            entries = entries
                .Where(timeEntry =>
                !(timeEntry.Date == entryToDelete.Date
                && timeEntry.Begin == entryToDelete.Begin
                && timeEntry.End == entryToDelete.End))
                .ToArray();

            return await WriteAllAsync(entries);
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

        public async Task<Result> UpdateAsync(TimeEntryRequest entryRequest)
        {
            var entriesResult = await GetAllAsync();

            if (entriesResult.IsFailure)
                return Result.Failure("Failed to retrieve existing entries.");

            var entries = entriesResult.Value;

            if (!entries.Any(e => e.Date == entryRequest.Date && e.Begin == entryRequest.Begin && e.End == entryRequest.End))
                return Result.Failure("Entry not found to update.");

            var entry = entries.FirstOrDefault(e => e.Date == entryRequest.Date && e.Begin == entryRequest.Begin && e.End == entryRequest.End);

            return entry is not null
                ? await WriteAllAsync(entries.Append(entry).ToArray())
                : Result.Failure("Entry not found to update.");
        }

        private async Task<Result> WriteAllAsync(IEnumerable<TimeEntry> entries)
        {
            try
            {
                var json = JsonSerializer.Serialize(entries);
                await File.WriteAllTextAsync(FilePath, json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                await Console.Out.WriteLineAsync($"{ex}");
                return Result.Failure("Failed to write to the file.");
            }
        }
    }
}