namespace YouTrack.For.ReSharper.Browser
{
    #region Using Directives

    using System;

    using JetBrains.CommonControls;
    using JetBrains.ReSharper.Features.Common.TreePsiBrowser;
    using JetBrains.TreeModels;
    using JetBrains.UI;
    using JetBrains.UI.TreeView;

    using YouTrack.For.ReSharper.SearchAction;

    #endregion

    public class YouTrackIssuePresenter : TreeModelBrowserPresenter
    {
        protected override void PresentObject(object value, IPresentableItem item, TreeModelNode modelNode, PresentationState state)
        {
            if (modelNode.Parent == null)
            {
                item.RichText.Text = "Issues";
                item.Images.Add(ImageLoader.GetImage("youtrack-root"));
            } 
            else
            {
                var issueItem = (IssueItem)value;

                item.RichText.Text = string.Format("[{0} - {1}] {2}", issueItem.Id, issueItem.State, issueItem.Summary);

                item.Images.Add(issueItem.Priority == IssueItemPriority.High
                                    ? ImageLoader.GetImage("issue-high-priority")
                                    : ImageLoader.GetImage("issue-low-priority"));
            }
        }
    }
}