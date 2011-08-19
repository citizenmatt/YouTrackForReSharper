namespace YouTrack.For.ReSharper.Infrastructure
{
    #region Using Directives

    using System.Drawing;

    using JetBrains.Application.Components;
    using JetBrains.Application.ComponentModel;
    using JetBrains.IDE.TreeBrowser;
    using JetBrains.ProjectModel;
    using JetBrains.UI.Controls;
    using JetBrains.UI.RichText;

    using YouTrack.For.ReSharper.SearchAction;

    #endregion
    
    [SolutionComponent(ProgramConfigurations.VS_ADDIN)]
    public class YouTrackSolutionInitialization : ISolutionComponent
    {
        private readonly ISolution solution;

        public YouTrackSolutionInitialization(ISolution solution)
        {
            this.solution = solution;
        }

        public void AfterSolutionOpened()
        {
            var emptyLabel = new RichTextLabel { BackColor = SystemColors.Control };
            emptyLabel.RichTextBlock.Add(new RichText("YouTrack Search Results", new TextStyle(FontStyle.Bold)));
            var browser1 = TreeModelBrowser.GetInstance(this.solution);
            browser1.RegisterBrowserWindow(YouTrackSearchAction.YouTrackBrowserWindowId, emptyLabel);
        }

        public void BeforeSolutionClosed()
        {
        }

        public void Dispose()
        {
        }

        public void Init()
        {
        }
    }
}