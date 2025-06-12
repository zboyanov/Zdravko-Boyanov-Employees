namespace BuddiesOnTheSameJob.Models
{
    /// <summary>
    /// Представлява двойка служители и броя дни, които са работили заедно.
    /// </summary>
    public sealed class BuddyPair
    {
        public int EmpId1 { get; set; }
        public int EmpId2 { get; set; }
        public int ProjectId { get; set; }
        public int DaysWorked { get; set; }
    }
}
