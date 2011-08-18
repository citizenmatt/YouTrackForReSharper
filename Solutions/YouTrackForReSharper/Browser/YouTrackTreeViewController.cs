namespace YouTrack.For.ReSharper.Browser
{
    #region Using Directives

    using JetBrains.CommonControls;
    using JetBrains.IDE.TreeBrowser;
    using JetBrains.ProjectModel;
    using JetBrains.TreeModels;
    using JetBrains.UI.TreeView;

    #endregion

    public class YouTrackTreeViewController : TreeModelBrowserDescriptor
    {
        private readonly TreeModel treeModel;
        private readonly YouTrackIssuePresenter presenter;
        private readonly ISolution solution;

        public YouTrackTreeViewController(ISolution solution, TreeSimpleModel treeModel) : base(solution)
        {
            this.treeModel  = treeModel;
            this.solution = solution;
            this.presenter = new YouTrackIssuePresenter();
        }

        public override ISolution Solution
        {
            get { return this.solution;  }
        }

        public override string Title
        {
            get { return "YouTrack"; }
        }

        public override TreeModel Model
        {
            get { return this.treeModel; }
        }

        public override StructuredPresenter<TreeModelNode, IPresentableItem> Presenter
        {
            get { return this.presenter; }
        }
    }
}