namespace YouTrack.For.ReSharper.Browser
{
    #region Using Directives

    using JetBrains.CommonControls;
    using JetBrains.TreeModels;
    using JetBrains.UI.TreeView;

    #endregion

    public class YouTrackIssueView : TreeModelPresentableView
    {
        public YouTrackIssueView(TreeModel model, ITreeViewController controller) : base(model, controller)
        {
        }

        public YouTrackIssueView(ITreeViewController controller) : base(controller)
        {
        }

        protected override void InitializeCells(TreeModelViewNode viewNode, TreeModelNode modelNode)
        {
            viewNode.SetValue(this.ModelColumn, new PresentableItem());
        }

        protected override void UpdateNodeCells(TreeModelViewNode viewNode, TreeModelNode modelNode, PresentationState state)
        {
            var presentableItem = viewNode.GetCellValue(this.ModelColumn) as IPresentableItem;

            if (presentableItem == null)
            {
                return;
            }

            presentableItem.Clear();
            this.Presenter.UpdateItem(modelNode, presentableItem, state);
            this.InvalidateNode(viewNode);
        }
    }
}