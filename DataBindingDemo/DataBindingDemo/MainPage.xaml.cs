using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace DataBindingDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<Item> l;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            l = new List<Item>();


            l.Add(new Item { bigtext = "hadhfd" , smalltext = "adfd" });
            l.Add(new Item { bigtext = "akdfjdh", smalltext = "aekurhekf" });
            l.Add(new Item { bigtext = "adfld", smalltext = "adfjldjfl" });
            ListBox_Items.ItemsSource = l; 

        }
    }

    public class Item
    {
        public string bigtext { get; set; }
        public string smalltext { get; set; }
    }
}