namespace YouTrack.For.ReSharper.Infrastructure
{
    #region Using Directives

    using System;
    using System.Windows.Forms;

    using YouTrack.For.ReSharper.Infrastructure.Options;
    using YouTrack.For.ReSharper.Properties;

    #endregion

    public partial class YouTrackSettingsForm : Form
    {
        private readonly YouTrackSettings youTrackSettings;
        private readonly YouTrackSettingsControl youTrackSettingsControl;
        private bool continueClicked;

        public YouTrackSettingsForm(YouTrackSettings youTrackSettings)
        {
            InitializeComponent();

            this.youTrackSettings = youTrackSettings;
            this.youTrackSettingsControl = new YouTrackSettingsControl(youTrackSettings);

            Controls.Add(this.youTrackSettingsControl);
        }
      
        private void YouTrackSettingsFormFormClosing(object sender, FormClosingEventArgs e)
        {
            this.youTrackSettingsControl.SaveSettings();

            if (this.continueClicked)
            {
                try
                {
                    this.youTrackSettings.ValidateSettings();
                }
                catch (YouTrackException exception)
                {
                    MessageBox.Show(
                        exception.Message,
                        Resources.YouTrackSettingsFormYouTrackSettings,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        
                    e.Cancel = true;
                }
            }
        }

        private void ButtonContinueClick(object sender, EventArgs e)
        {
            this.continueClicked = true;
        }
    }
}