namespace YouTrack.For.ReSharper.SearchAction
{
    #region Using Directives

    using System.Windows.Forms;

    using JetBrains.ActionManagement;
    using JetBrains.Application.DataContext;

    using YouTrack.For.ReSharper.Infrastructure;
    using YouTrack.For.ReSharper.Properties;

    using DataConstants = JetBrains.IDE.DataConstants;

    #endregion

    [ActionHandler]
    public class YouTrackSearchAction : IActionHandler
    {
        public const string YouTrackBrowserWindowId = "YouTrackBrowserWindowID";

        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            // It's always allowed. We don't need a solution present
            return context.CheckAllNotNull(DataConstants.SOLUTION);
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            var solution = context.GetData(DataConstants.SOLUTION);


            if (solution == null)
            {
                MessageBox.Show(
                    Resources.YouTrackSearchActionExecuteYouNeedASolutionOpen,
                    "YouTrack",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                var youTrackSettings = new YouTrackSettings(solution);

                this.ValidateYouTrackSettings(youTrackSettings);

                var connection = new YouTrackServer(youTrackSettings);

                var youTrackSearch = new YouTrackSearch(solution, connection);

                youTrackSearch.Search();
            }
        }

        private void ValidateYouTrackSettings(YouTrackSettings youTrackSettings)
        {
            try
            {
                youTrackSettings.ValidateSettings();
            }
            catch (YouTrackException)
            {
                var youtrackSettingsForm = new YouTrackSettingsForm(youTrackSettings);

                if (youtrackSettingsForm.ShowDialog() == DialogResult.OK)
                {
                    youTrackSettings.Save();
                }
            }
        }
    }
}