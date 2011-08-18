namespace YouTrack.For.ReSharper.SearchAction
{
    using System.Windows.Forms;

    public partial class SearchBox : Form
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        public string SearchString
        {
            get { return textFilter.Text; }
        }

        private void SearchBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
