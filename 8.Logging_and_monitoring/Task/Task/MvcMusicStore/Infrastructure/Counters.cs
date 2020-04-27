using PerformanceCounterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Infrastructure
{
    [PerformanceCounterCategory("MvcMusicStore", 
        System.Diagnostics.PerformanceCounterCategoryType.MultiInstance, 
        "MvcMusicStore")]
    public enum Counters
    {
        [PerformanceCounter("Go to home counter", "Go to home", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        GoToHome
    }
}