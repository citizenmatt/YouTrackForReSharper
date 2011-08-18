namespace YouTrack.For.ReSharper.Browser
{
    #region Using Directives

    using System.Windows.Forms;

    using JetBrains.ActionManagement;
    using JetBrains.CommonControls;
    using JetBrains.IDE.TreeBrowser;
    using JetBrains.TreeModels;
    using JetBrains.UI.TreeView;

    using YouTrack.For.ReSharper.SearchAction;

    #endregion

    public class YouTrackTreeModelPanel : TreeModelBrowserPanel
    {
        private readonly YouTrackStatusPanel statusPanel;

        private readonly YouTrackTreeViewController controller;

        private YouTrackIssueView issueView;

        public YouTrackTreeModelPanel(YouTrackTreeViewController controller) : base(controller)
        {
            this.controller = controller;
            this.statusPanel = new YouTrackStatusPanel
            {
                Dock = DockStyle.Top,
                Height = 10
            };
        }

        protected override void Dispose(bool disposing)
        {
        }

        protected override StructuredPresenter<TreeModelNode, IPresentableItem> GetPresenter()
        {
            return this.controller.Presenter;
        }

        protected override IActionBar CreateActionBar(string actionId)
        {
            return ActionBarManager.Instance.CreateActionBar(actionId, this, true);
        }

        protected override TreeModelPresentableView CreateView(TreeModel model)
        {
            this.issueView = new YouTrackIssueView(TreeModel, Descriptor);
            return this.issueView;
        }

        protected override void InitializeCustomBar()
        {
            base.InitializeCustomBar();
            Controls.Add(this.statusPanel);
        }
    }
}