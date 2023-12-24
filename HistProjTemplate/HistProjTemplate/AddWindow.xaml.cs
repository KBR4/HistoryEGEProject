using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using HistLib;
using System.IO;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private List<Sect> Sections;
        public AddWindow()
        {
            InitializeComponent();
            SectionList.SelectedItem = null;
            Sections = (List<Sect>)Deserialization();

            if (Sections == null)
            {
                Sections = new List<Sect>();
            }

            if (Sections.Count > 0)
            {
                UpdateSectionList();
            }
            else
            {
                DelB.IsEnabled = false;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            PassWindow pw = new PassWindow("Введите название раздела, который вы хотите добавить:");
            if (pw.ShowDialog() == true)
            {
                string NewSectName = pw.Password;

                bool SectionAlreadyExists = false;
                foreach (Sect s in Sections)
                {
                    if (s.Name == NewSectName)
                    {
                        MessageBox.Show("Раздел с таким названием уже существует");
                        SectionAlreadyExists = true;
                        break;
                    }
                }
                if (!SectionAlreadyExists)
                {
                    AddSectByUser(NewSectName);
                    UpdateSectionList();
                }              
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (SectionList.SelectedItem != null)
            {
                string ChosenSectName = SectionList.SelectedItem.ToString();
                ConfirmWindow cw = new ConfirmWindow("Вы уверены что хотите удалить выбранный раздел со всеми тестами?");
                if (cw.ShowDialog() == true)
                {
                    if (GetSectByName(Sections, ChosenSectName) != null)
                    {
                        RemoveSectByUser(ChosenSectName);
                        UpdateSectionList();
                    }

                    //RemoveSectByUser(ChosenSectName);
                    //UpdateSectionList();
                }
            }
        }
        private void AddTest_Click(object sender, RoutedEventArgs e)
        {
            if (SectionList.SelectedItem == null)
            {
                MessageBox.Show("Выберите раздел, в который нужно добавить тест");
            }
        }

        private void UpdateSectionList()        //Функция обновления визуального представления листа с разделами
        {
            SectionList.Items.Clear();
            foreach (Sect s in Sections)
            {
                SectionList.Items.Add(s.ToString());
            }
            if (Sections.Count == 0)
            {
                DelB.IsEnabled = false;
            }
            else
            {
                DelB.IsEnabled = true;
            }
        }

        private Sect GetSectByName (List<Sect> ls, string Name)   //Поиск раздела по названию TO DO: (убрать отсюда)
        {
            foreach (Sect s in ls)
            {
                if (s.Name == Name)
                {
                    return s;
                }
            }
            return null;
        }
        //методы сериализации и десериализации
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

        // добавление нового раздела 
        public void AddSectByUser(string Name)
        {
            Sections = (List<Sect>)Deserialization();
            if (Sections == null)
            {
                Sections = new List<Sect>();
            }
            Sect newSect = new Sect(Name);
            Sections.Add(newSect);
            Serialization(Sections);
        }

        // удаление раздела по имени (удаляет все разделы с данным именем)
        public void RemoveSectByUser(string Name)
        {
            Sections = (List<Sect>)Deserialization();
            if (Sections == null)
            {
                Sections = new List<Sect>();
            }
            for (int i = 0; i < Sections.Count; i++)
            {
                if (Sections[i].Name == Name)
                {
                    Sections.RemoveAt(i);
                }
            }
            Serialization(Sections);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
