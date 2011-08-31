using JetBrains.DataFlow;
using JetBrains.UI.ToolWindowManagement;

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

    [SolutionComponent]
    public class YouTrackSearch
    {
        readonly Lifetime _lifetime;
        private readonly ISolution solution;
        private readonly YouTrackServer youTrackServer;

        public YouTrackSearch(Lifetime lifetime, ISolution solution, YouTrackServer youTrackServer)
        {
            _lifetime = lifetime;
            this.solution = solution;
            this.youTrackServer = youTrackServer;
        }

        public void Search()
        {
            using (var searchBox = new SearchBox())
            {
                if (searchBox.ShowDialog(solution.GetComponent<IMainWindow>()) == DialogResult.OK)
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
                    var toolWindowManager = solution.GetComponent<ToolWindowManager>();
                    var descriptor = solution.GetComponent<YouTrackToolWindowDescriptor>();
                    var toolWindowClass = toolWindowManager.Classes[descriptor];
                    var toolWindowInstance = toolWindowClass.RegisterInstance(_lifetime, "EMPTY TITLE", null, (lifetime, instance) => browserPanel);
                    toolWindowInstance.Lifetime.AddAction(() => browserPanel.Dispose());
                    toolWindowInstance.EnsureControlCreated().Show();
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

            return issueManagement.GetAllIssuesForProject(this.youTrackServer.Project);
        }
    }
}