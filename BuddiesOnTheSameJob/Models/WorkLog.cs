namespace BuddiesOnTheSameJob.Models
{
    /// <summary>
    /// Представлява запис за работа на служител по проект.
    /// </summary>
    public sealed class WorkLog
    {
        public int EmpId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
