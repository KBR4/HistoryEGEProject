﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Reflection;
using Microsoft.Win32;

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
            Sections = (List<Sect>)Deserialization();
            UpdateSectionList();
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
                obj = formatter.Deserialize(fs);
                return obj;
            }
        }

        // добавление нового раздела 
        public void AddSectByUser(string Name)
        {
            Sections = (List<Sect>)Deserialization();
            Sect newSect = new Sect(Name);
            Sections.Add(newSect);
            Serialization(Sections);
        }

        // удаление раздела по имени (удаляет все разделы с данным именем)
        public void RemoveSectByUser(string Name)
        {
            Sections = (List<Sect>)Deserialization();
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

        private void SectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}