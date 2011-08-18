namespace YouTrack.For.ReSharper.SearchAction
{
    public class IssueItem
    {
        public string Id { get; set; }

        public string Summary { get; set; }

        public bool IsResolved { get; set; }

        public IssueItemPriority Priority { get; set; }

        public string State { get; set; }
    }
}