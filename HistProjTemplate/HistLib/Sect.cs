﻿using System;
using System.Collections.Generic;

namespace HistLib
{
    //Пока что оставляю все публичным. С грамотной инкапсуляцией разберемся при рефакторинге.
    [Serializable]
    public class Sect   //Класс раздел. Каждому разделу соответствует лист тестов по нему. (возможно с разными картами)
    {
        public string Name { get; set; }    //название раздела
        public List<Test> Tests;            //лист для хранения тестов
        public Sect(string s)               //конструктор для создания раздела по названию
        {
            Name = s;
            Tests = new List<Test>();
        }
        public Sect(string s, List<Test> lt)//конструктор для создания раздела по названию и листу тестов - для десериализованного варианта
        {
            Tests = new List<Test>();
            Name = s;
            Tests = lt;
        }
        public void AddTest(Test t)         //функция добавления теста в раздел (возможно не нужна)
        {
            Tests.Add(t);
        }
        public override string ToString()
        {
            return Name;
        }
    }   
}
