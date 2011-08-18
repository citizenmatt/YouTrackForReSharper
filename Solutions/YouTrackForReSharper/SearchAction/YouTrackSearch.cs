namespace YouTrack.For.ReSharper.SearchAction
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using JetBrains.IDE.TreeBrowser;
    using JetBrains.ProjectModel;
    using JetBrains.TreeModels;
    using JetBrains.UI.Application;

    using YouTrack.For.ReSharper.Browser;
    using YouTrack.For.ReSharper.Infrastructure;

    using YouTrackSharp.Issues;

    #endregion

    public class YouTrackSearch
    {
        private readonly ISolution solution;
        private readonly YouTrackServer youTrackServer;

        public YouTrackSearch(ISolution solution, YouTrackServer youTrackServer)
        {
            this.solution = solution;
            this.youTrackServer = youTrackServer;
        }

        public void Search()
        {
            using (var searchBox = new SearchBox())
            {
                if (searchBox.ShowDialog(UIApplicationShell.Instance.MainWindow) == DialogResult.OK)
                {
                    IEnumerable<Issue> issues = null;

                    try
                    {
                        issues = this.PerformSearch();
                    }
                    catch (Exception exception)
                    {
                    }

                    var model = this.PrepareTreeModel(issues);

                    var controller = new YouTrackTreeViewController(this.solution, model);
                    var browserPanel = new YouTrackTreeModelPanel(controller);
                    var browser = TreeModelBrowser.GetInstance(this.solution);

                    browser.Show(YouTrackSearchAction.YouTrackBrowserWindowId, controller, browserPanel);
                }
            }
        }

        private TreeSimpleModel PrepareTreeModel(IEnumerable<Issue> issues)
        {
            var model = new TreeSimpleModel();

            var parent = new IssueItem();

            model.Insert(null, parent);

            foreach (var issue in issues)
            {
                var issueItem = new IssueItem
                                {
                                    Id = issue.Id,
                                    Priority = this.youTrackServer.GetPriortyByName(issue.Priority),
                                    Summary = issue.Summary,
                                    IsResolved = false,
                                    State = issue.State,
                        };

                foreach (var closedIssue in this.youTrackServer.ResolvedStates)
                {
                    if (closedIssue.Name == issue.State)
                    {
                        issueItem.IsResolved = true;
                        break;
                    }
                }

                model.Insert(parent, issueItem);
            }

            return model;
        }

        private IEnumerable<Issue> PerformSearch()
        {
            var connection = this.youTrackServer.Connect();

            var issueManagement = new IssueManagement(connection);

            return issueManagement.GetIssues(this.youTrackServer.Project);
        }
    }
}