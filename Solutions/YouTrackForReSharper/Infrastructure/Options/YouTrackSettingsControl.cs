namespace YouTrack.For.ReSharper.Infrastructure.Options
{
    #region Using Directives

    using System;
    using System.Windows.Forms;

    #endregion

    public partial class YouTrackSettingsControl : UserControl
    {
        private readonly YouTrackSettings youTrackSettings;

        public YouTrackSettingsControl(YouTrackSettings youTrackSettings)
        {
            InitializeComponent();

            this.youTrackSettings = youTrackSettings;

            this.LoadSettings();

            textHost.Focus();
        }

        public void LoadSettings()
        {
            checkUseSSL.Checked = false;

            textHost.Text = this.youTrackSettings.Host;
            textPort.Text = this.youTrackSettings.Port.ToString();
            checkUseSSL.Checked = this.youTrackSettings.UseSSL;
            textProject.Text = this.youTrackSettings.Project;

            textUsername.Text = this.youTrackSettings.Username;
            textPassword.Text = this.youTrackSettings.Password;
        }

        public void SaveSettings()
        {
            this.youTrackSettings.Host = textHost.Text;
            this.youTrackSettings.Port = Convert.ToInt32(textPort.Text);
            this.youTrackSettings.UseSSL = checkUseSSL.Checked;
            this.youTrackSettings.Project = textProject.Text;

            this.youTrackSettings.Username = textUsername.Text;
            this.youTrackSettings.Password = textPassword.Text;
        }
    }
}