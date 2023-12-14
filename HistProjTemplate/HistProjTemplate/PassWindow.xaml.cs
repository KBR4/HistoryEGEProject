using System.Windows;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for PassWindow.xaml
    /// </summary>
    public partial class PassWindow : Window
    {
        public PassWindow()
        {
            InitializeComponent();
        }
        public PassWindow(string s)
        {
            InitializeComponent();
            TextInfo.Text = s;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Password
        {
            get { return passwordBox.Text; }
        }
    }
}
