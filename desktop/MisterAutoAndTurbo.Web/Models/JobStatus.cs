namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Status values used for job cards.
    /// </summary>
    public static class JobStatus
    {
        public const string Pending = "Pending";
        public const string InProgress = "In Progress";
        public const string WaitingParts = "Waiting for Parts";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";

        public static readonly string[] All = { Pending, InProgress, WaitingParts, Completed, Cancelled };
    }
}
