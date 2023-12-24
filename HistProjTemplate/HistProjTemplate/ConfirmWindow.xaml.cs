using System.Windows;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        public ConfirmWindow()
        {
            InitializeComponent();
        }
        public ConfirmWindow(string s)
        {
            InitializeComponent();
            QuestionBlock.Text = s;          
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
