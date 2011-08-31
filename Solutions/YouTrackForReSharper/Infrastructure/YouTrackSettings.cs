namespace YouTrack.For.ReSharper.Infrastructure
{
    #region Using Directives

    using JetBrains.ProjectModel;
    using JetBrains.Util;

    using YouTrack.For.ReSharper.Properties;

    #endregion

[SolutionComponent]
    public class YouTrackSettings
    {
        private readonly ISolution solution;

        public YouTrackSettings(ISolution solution)
        {
            this.solution = solution;
            this.Load();
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSSL { get; set; }

        public string Project { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int PriorityBarrier { get; set; }

        public void ValidateSettings()
        {
             if (string.IsNullOrEmpty(this.Host) || string.IsNullOrEmpty(this.Project))
             {
                 throw new YouTrackException(Resources.YouTrackSettingsValidateSettingsHostAndProjectARequired);
             }
        }

        public void Save()
        {
            SolutionSettingsTable.GetInstance(this.solution).SetString("YouTrackHostName", this.Host);
            SolutionSettingsTable.GetInstance(this.solution).SetInteger("YouTrackPort", this.Port);
            SolutionSettingsTable.GetInstance(this.solution).SetBoolean("YouTrackUseSSL", this.UseSSL);
            SolutionSettingsTable.GetInstance(this.solution).SetString("YouTrackProject", this.Project);

            SolutionSettingsTable.GetInstance(this.solution).SetString("YouTrackUsername", this.Username);
            SolutionSettingsTable.GetInstance(this.solution).SetEncryptedString("YouTrackPassword", this.Password);
        }

        private void Load()
        {
            this.Host  = SolutionSettingsTable.GetInstance(this.solution).GetString("YouTrackHostName");
            this.Port = SolutionSettingsTable.GetInstance(this.solution).GetInteger("YouTrackHostPort", 80);
            this.UseSSL = SolutionSettingsTable.GetInstance(this.solution).GetBoolean("YouTrackHostName", false);
            this.Project = SolutionSettingsTable.GetInstance(this.solution).GetString("YouTrackProject");
            this.PriorityBarrier = SolutionSettingsTable.GetInstance(this.solution).GetInteger("YouTrackPriorityBarrier", 2);

            this.Username = SolutionSettingsTable.GetInstance(this.solution).GetString("YouTrackUsername");
            this.Password = SolutionSettingsTable.GetInstance(this.solution).GetEncryptedString("YouTrackPassword");
        }
    }
}