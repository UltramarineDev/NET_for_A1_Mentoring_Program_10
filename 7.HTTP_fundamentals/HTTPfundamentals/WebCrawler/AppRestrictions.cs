namespace WebCrawler
{
    public class AppRestrictions
    {
        public AppRestrictions(int linkAnalysisDepth, DomainTransitionOptions domainTransitionOption, string[] dowloadResourceExtensions, bool verbose)
        {
            LinkAnalysisDepth = linkAnalysisDepth;
            DomainTransitionOption = domainTransitionOption;
            DowloadResourceExtensions = dowloadResourceExtensions;
            Verbose = verbose;
        }

        public int LinkAnalysisDepth { get; }
        public DomainTransitionOptions DomainTransitionOption { get; }
        public string[] DowloadResourceExtensions { get; }
        public bool Verbose { get; }
    }
}
