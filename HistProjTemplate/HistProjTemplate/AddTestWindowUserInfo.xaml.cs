using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HistLib;
namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for AddTestWindowUserInfo.xaml
    /// </summary>
    public partial class AddTestWindowUserInfo : Window
    {
        public Test AddedTest;
        public AddTestWindowUserInfo()
        {
            InitializeComponent();
        }

        private void AddMapClick(object sender, RoutedEventArgs e) //добавить картинку, отправить ее в имагес, показать превью на экране
        {
            //Егор - сделать
        }

        private void AddTXTFileClick(object sender, RoutedEventArgs e) //добавить тхт файл, сгенерировать из него массив QuestionsAnswers
        {
            //Егор - сделать
        }

        private void NextQClick(object sender, RoutedEventArgs e) //к следующему вопросу (вопросы подгружены из тхт
        {

        }

        private void PrevQClick(object sender, RoutedEventArgs e) //к предыдущему вопросу (вопросы подгружены из тхт)
        {

        }

        private void AddTestClick(object sender, RoutedEventArgs e) //кнопка возврата к предыдущему окну
        {
            //Егор - сделать

            // AddedTest = ... //эта штука возвращает добавленный тест предыдущему окну
            this.DialogResult = true;
        }

        private void GobBackClick(object sender, RoutedEventArgs e) //назад
        {
            this.DialogResult = false;
        }
    }
}
