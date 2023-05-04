using System;

namespace Framework
{
    public static class Pathes
    {
        public static string SOLUTION_PATH = AppDomain.CurrentDomain.BaseDirectory
            .Replace("\\packages\\bin\\agents\\net7.0", "")
            .Replace("\\Booking.Tests\\bin\\Debug\\net7.0", "");
        public static string TESTS_PATH = SOLUTION_PATH + "\\Booking.Tests";
        public static string MODELS_PATH = SOLUTION_PATH + "\\Booking.Models\\bin\\Debug\\net6.0";
        public static string SCREENSHOTS_PATH = TESTS_PATH + "\\Screenshots";
        public static string REPORTS_PATH = TESTS_PATH + "\\Report";
    }
}
