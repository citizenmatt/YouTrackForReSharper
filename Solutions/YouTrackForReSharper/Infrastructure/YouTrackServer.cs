using JetBrains.ProjectModel;

namespace YouTrack.For.ReSharper.Infrastructure
{
    #region Using Directives

    using System.Collections.Generic;

    using YouTrack.For.ReSharper.SearchAction;

    using YouTrackSharp.Infrastructure;
    using YouTrackSharp.Projects;

    #endregion

    [SolutionComponent]
    public class YouTrackServer
    {
        private readonly YouTrackSettings youTrackSettings;

        public YouTrackServer(YouTrackSettings youTrackSettings)
        {
            this.youTrackSettings = youTrackSettings;
            this.Project = this.youTrackSettings.Project;
        }

        public string Project { get; private set; }

        public IEnumerable<ProjectState> ProjectStates { get; private set; }

        public IEnumerable<ProjectResolutionType> ResolvedStates { get; private set; }

        public IEnumerable<ProjectPriority> ProjectPriorities { get; private set; }

        public Connection Connect()
        {
            var connection = new Connection(
                this.youTrackSettings.Host, 
                this.youTrackSettings.Port, 
                this.youTrackSettings.UseSSL);

            if (!string.IsNullOrEmpty(this.youTrackSettings.Username))
            {
                connection.Authenticate(this.youTrackSettings.Username, this.youTrackSettings.Password);

                var projectManagement = new ProjectManagement(connection);

                this.ProjectStates = projectManagement.GetStates();
                this.ResolvedStates = projectManagement.GetResolutions();
                this.ProjectPriorities = projectManagement.GetPriorities();
            }

            return connection;
        }

        public IssueItemPriority GetPriortyByName(string priority)
        {
            foreach (var projectPriority in this.ProjectPriorities)
            {
                if (projectPriority.Name == priority)
                {
                    return projectPriority.NumericValue >= this.youTrackSettings.PriorityBarrier ? IssueItemPriority.High : IssueItemPriority.Low;
                }
            }

            return IssueItemPriority.Low;
        }
    }
}