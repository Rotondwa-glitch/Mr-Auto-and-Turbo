namespace MisterAutoAndTurbo.Web.Services
{
    /// <summary>
    /// South African time helpers (SAST, UTC+2, no DST).
    /// </summary>
    public static class SaTime
    {
        private static readonly TimeZoneInfo Zone = Find();

        private static TimeZoneInfo Find()
        {
            foreach (var id in new[] { "Africa/Johannesburg", "South Africa Standard Time" })
            {
                try { return TimeZoneInfo.FindSystemTimeZoneById(id); } catch { }
            }
            return TimeZoneInfo.CreateCustomTimeZone("SAST", TimeSpan.FromHours(2), "SAST", "SAST");
        }

        public static DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Zone);

        public static DateTime ToSa(this DateTime utc)
        {
            if (utc == default) return utc;
            var u = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(u, Zone);
        }

        public static DateTime? ToSa(this DateTime? utc) => utc.HasValue ? utc.Value.ToSa() : null;

        public static DateTime FromSa(DateTime sa)
        {
            if (sa == default) return sa;
            var u = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(sa, DateTimeKind.Unspecified), Zone);
            return DateTime.SpecifyKind(u, DateTimeKind.Unspecified);
        }
    }
}
