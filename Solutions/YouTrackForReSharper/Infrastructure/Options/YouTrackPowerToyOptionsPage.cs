namespace YouTrack.For.ReSharper.Infrastructure.Options
{
    #region Using Directives

    using System.Windows.Forms;

    using JetBrains.IDE;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Features.Common.Options;
    using JetBrains.UI.Controls;
    using JetBrains.UI.CrossFramework;
    using JetBrains.UI.Options;

    #endregion

    [OptionsPage(OptionId, "YouTrack", "YouTrack.For.ReSharper.Resources.youtrack-sample.png", ParentId = ToolsPage.PID)]
    public partial class YouTrackPowerToyOptionsPage : UserControl, IOptionsPage
    {
        private readonly ISolution solution;
        private readonly YouTrackSettingsControl youTrackSettingsControl;
        private readonly YouTrackSettings youTrackSettings;
        
        private const string OptionId = "YouTrackPowerToy-35C1F372-1A77-428D-8074-7CFBBA865A04";

        public YouTrackPowerToyOptionsPage(IOptionsDialog optionsDialog, ISolution solution)
        {
            this.solution = solution;

            if (this.solution != null)
            {
                InitializeComponent();
                this.youTrackSettings = solution.GetComponent<YouTrackSettings>();
                this.youTrackSettingsControl = new YouTrackSettingsControl(this.youTrackSettings);

                this.Controls.Add(this.youTrackSettingsControl);
            }
            else
            {
                Controls.Add(JetBrains.UI.Options.Helpers.Controls.CreateNoSolutionCueBanner());
                //Controls.Add(new RichTextLabel("You need to have an open solution to access these settings"));
            }
        }

        public bool OnOk()
        {
            this.youTrackSettingsControl.SaveSettings();
            this.youTrackSettings.Save();
            
            return true;
        }

        public bool ValidatePage()
        {
            return true;
        }

        public EitherControl Control
        {
            get { return this; }
        }

        public string Id
        {
            get { return OptionId; }
        }
    }
}