using BuddiesOnTheSameJob.Models;

namespace BuddiesOnTheSameJob.Interfaces
{
    public interface IFileParser
    {
        List<WorkLog> Parse(string filePath);
    }   
}
