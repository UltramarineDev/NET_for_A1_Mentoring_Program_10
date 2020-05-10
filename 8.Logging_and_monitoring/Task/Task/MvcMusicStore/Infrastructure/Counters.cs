using System.Diagnostics;

namespace MvcMusicStore.Infrastructure
{
    public static class Counters
    {
        public static PerformanceCounter GoToHome { get; set; }
        public static PerformanceCounter LogIn { get; set; }
        public static PerformanceCounter LogOff { get; set; }
    }
}