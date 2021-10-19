using System.Collections.Generic;

namespace PersonalizerMobileDemo.Services
{
    public class OptionsService
    {
        public static List<string> GetTimes() => new List<string>() { "morning", "afternoon", "evening", "night" };
        public static List<string> GetTastes() => new List<string> { "salty", "sweet" };
    }
}
