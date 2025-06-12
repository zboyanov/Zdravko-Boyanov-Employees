using BuddiesOnTheSameJob.Models;

namespace BuddiesOnTheSameJob.Interfaces
{
    public interface IBuddyAnalyzer
    {
        List<BuddyPair> GetLongestWorkingPairs(List<WorkLog> workLogs);
    }
}
