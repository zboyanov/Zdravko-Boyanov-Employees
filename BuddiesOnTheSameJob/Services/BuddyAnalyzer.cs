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

            var result = new List<BuddyPair>();
            var workLogsByProject = workLogs.GroupBy(w => w.ProjectId);

            foreach (var projectGroup in workLogsByProject)
            {
                var logs = projectGroup.ToList();
                BuddyPair maxPair = null;
                int maxDays = 0;

                for (int i = 0; i < logs.Count; i++)
                {
                    for (int j = i + 1; j < logs.Count; j++)
                    {
                        var emp1 = logs[i];
                        var emp2 = logs[j];

                        var overlapStart = emp1.DateFrom > emp2.DateFrom ? emp1.DateFrom : emp2.DateFrom;
                        var overlapEnd = emp1.DateTo < emp2.DateTo ? emp1.DateTo : emp2.DateTo;

                        if (overlapStart < overlapEnd)
                        {
                            var overlapDays = (overlapEnd.Value - overlapStart).Days;

                            if (overlapDays > maxDays)
                            {
                                maxDays = overlapDays;
                                maxPair = new BuddyPair
                                {
                                    EmpId1 = Math.Min(emp1.EmpId, emp2.EmpId),
                                    EmpId2 = Math.Max(emp1.EmpId, emp2.EmpId),
                                    ProjectId = emp1.ProjectId,
                                    DaysWorked = overlapDays
                                };
                            }
                        }
                    }
                }

                if (maxPair != null)
                {
                    result.Add(maxPair);
                }
            }

            return result;
        }
    }
}
