namespace Exams.Domain.Helper
{
    public static class Helper
    {
        public static DateTime GetCurrentDateTime()
            => DateTime.UtcNow.AddHours(5);
    }
}
