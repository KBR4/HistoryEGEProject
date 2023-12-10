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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.Reflection;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        private List<Sect> Sections;
        public AddTest()
        {
            InitializeComponent();
            TestView.SelectedItem = null;
            SectBox.SelectedItem = null;
            DelB.IsEnabled = false;
            AddB.IsEnabled = false;
            Sections = (List<Sect>)Deserialization();
            if (Sections == null || Sections.Count == 0)
            {
                MessageBox.Show("Сначала создайте разделы для тестов");
                Sections = new List<Sect>();
            }
            foreach (Sect s in Sections)
            {
                SectBox.Items.Add(s.ToString());
            }
        }


        public Sect GetSectByName(string Name)
        {
            foreach (Sect sec in Sections)
            {
                if (sec.Name == Name)
                {
                    return sec;
                }
            }
            return null;
        }

        public void Serialization(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            using (FileStream fs = new FileStream("sections.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }

        public object Deserialization()
        {
            BinaryFormatter formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            object obj = new object();
            using (FileStream fs = new FileStream("sections.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    obj = formatter.Deserialize(fs);
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddTestByUser(Test test)
        {
            if (SectBox.SelectedItem != null)
            {
                Sections = (List<Sect>)Deserialization();
                if (Sections == null)
                {
                    Sections = new List<Sect>();
                }
                string SectName = SectBox.SelectedItem.ToString();
                Sect s = GetSectByName(SectName);
                s.AddTest(test);
                Serialization(Sections);
                TestView.Items.Clear();
                foreach (Test t in s.Tests)
                {
                    TestView.Items.Add(t.Name);
                }
            }
            else
            {
                MessageBox.Show("Введите название раздела.");
            }
        }
        private void SectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Name = SectBox.SelectedItem.ToString();
            if (Name != null)
            {
                AddB.IsEnabled = true;

                Sect s = GetSectByName(Name);

                TestView.Items.Clear();
                foreach (Test t in s.Tests)
                {
                    TestView.Items.Add(t.Name);
                }
                if (s.Tests.Count > 0)
                {
                    DelB.IsEnabled = true;
                }
                else
                {
                    DelB.IsEnabled = false;
                }
            }
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)    //Добавить тест - новое окно спрашивает название
        {
            List<string> UsedNames = new List<string>();
            string SectName = SectBox.SelectedItem.ToString();
            Sect s = GetSectByName(SectName);
            if (s != null)
            {
                foreach (Test t in s.Tests)
                {
                    UsedNames.Add(t.Name);
                }
            }
            AddTestWindowUserInfo atw = new AddTestWindowUserInfo(UsedNames);
            if (atw.ShowDialog() == true)
            {
                Test newTest = atw.AddedTest;
                AddTestByUser(newTest);

                //Егор - сделать
                //newTest добавляется в нужный раздел и сериализуется
            }

            //Старая функция
            //PassWindow pw = new PassWindow("Введите название добавляемого теста:");
            //if (pw.ShowDialog() == true)
            //{
            //    string NewTestName = pw.Password;
            //    string SectName = SectBox.SelectedItem.ToString();
            //    AddTestByUser(NewTestName, SectName);
            //}
        }

        private void RemoveTest_Click(object sender, RoutedEventArgs e)  //Удалить выбранный тест
        {
            if (TestView.SelectedItem != null)
            {
                string TestName = TestView.SelectedItem.ToString();
                string SectName = SectBox.SelectedItem.ToString();
                RemoveTestByUser(SectName, TestName);
            }
            else
            {
                MessageBox.Show("Выберите тест, который вы хотите удалить");
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)     //Назад
        {
            this.DialogResult = true;
        }

        public void RemoveTestByUser(string SectName, string TestName)  //удаление теста из раздела с известным названием
        {
            Sections = (List<Sect>)Deserialization();
            if (Sections == null)
            {
                Sections = new List<Sect>();
            }
            Sect sect = null;
            for (int i = 0; i < Sections.Count; i++)
            {
                if (Sections[i].Name == SectName)
                {
                    sect = Sections[i];
                    break;
                }
            }
            for (int i = 0; i<sect.Tests.Count; i++)
            {
                if (sect.Tests[i].Name == TestName)
                {
                    sect.Tests.RemoveAt(i);
                    break;
                }
            }
            TestView.Items.Clear();
            foreach (Test t in sect.Tests)
            {
                TestView.Items.Add(t.Name);
            }
            Serialization(Sections);
        }

    }
}
