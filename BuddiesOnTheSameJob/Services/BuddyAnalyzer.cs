using BuddiesOnTheSameJob.Interfaces;
using BuddiesOnTheSameJob.Models;

namespace BuddiesOnTheSameJob.Services
{
    public class BuddyAnalyzer : IBuddyAnalyzer
    {
        public List<BuddyPair> GetLongestWorkingPairs(List<WorkLog> workLogs)
        {
            if (workLogs is null || workLogs.Count < 2)
                return new List<BuddyPair>();

            var pairs = new List<BuddyPair>();

            // Групиране по проект
            var groupedByProject = workLogs.GroupBy(w => w.ProjectId);

            foreach (var group in groupedByProject)
            {
                var projectLogs = group.ToList();

                for (int i = 0; i < projectLogs.Count; i++)
                {
                    for (int j = i + 1; j < projectLogs.Count; j++)
                    {
                        var emp1 = projectLogs[i];
                        var emp2 = projectLogs[j];

                        var overlapStart = emp1.DateFrom > emp2.DateFrom ? emp1.DateFrom : emp2.DateFrom;
                        var overlapEnd = emp1.DateTo < emp2.DateTo ? emp1.DateTo : emp2.DateTo;

                        if (overlapStart < overlapEnd)
                        {
                            int days = (overlapEnd - overlapStart).Value.Days;

                            pairs.Add(new BuddyPair
                            {
                                EmpId1 = emp1.EmpId,
                                EmpId2 = emp2.EmpId,
                                ProjectId = group.Key,
                                DaysWorked = days
                            });
                        }
                    }
                }
            }

            return pairs
                .GroupBy(p => new { p.EmpId1, p.EmpId2 })
                .Select(g => new BuddyPair
                {
                    EmpId1 = g.Key.EmpId1,
                    EmpId2 = g.Key.EmpId2,
                    ProjectId = g.First().ProjectId,
                    DaysWorked = g.Sum(p => p.DaysWorked)
                })
                .OrderByDescending(p => p.DaysWorked)
                .ToList();
        }
    }
}
