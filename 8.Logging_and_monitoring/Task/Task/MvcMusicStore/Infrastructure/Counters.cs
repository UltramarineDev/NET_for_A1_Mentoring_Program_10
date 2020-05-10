using System.Diagnostics;

namespace MvcMusicStore.Infrastructure
{
    public class Counters
    {
        public PerformanceCounter GoToHome { get; set; }
        public PerformanceCounter LogIn { get; set; }
        public PerformanceCounter LogOff { get; set; }

        public Counters()
        {
            this.GoToHome = new PerformanceCounter("MvcMusicStoreCategory", "NumberOfGoingToHome", false);
            this.LogIn = new PerformanceCounter("MvcMusicStoreCategory", "logInNumber", false);
            this.LogOff = new PerformanceCounter("MvcMusicStoreCategory", "logOffNumber", false);

            this.GoToHome.RawValue = 0;
            this.LogIn.RawValue = 0;
            this.LogOff.RawValue = 0;
        }
    }
}