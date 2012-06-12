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

namespace WCF_Demo
{
    public partial class MainPage : PhoneApplicationPage
    {
        ServiceReference1.Service1Client c;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            c = new ServiceReference1.Service1Client();
            c.getajokeCompleted += new EventHandler<ServiceReference1.getajokeCompletedEventArgs>(c_getajokeCompleted);
            c.GetDataCompleted += new EventHandler<ServiceReference1.GetDataCompletedEventArgs>(c_GetDataCompleted);
            c.GetDataUsingDataContractCompleted += new EventHandler<ServiceReference1.GetDataUsingDataContractCompletedEventArgs>(c_GetDataUsingDataContractCompleted);
        }

        void c_GetDataUsingDataContractCompleted(object sender, ServiceReference1.GetDataUsingDataContractCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if(!e.Cancelled)
            {
                textBlock_multihopResult.Text = e.Result.StringValue;
            }
               
        }


        void c_GetDataCompleted(object sender, ServiceReference1.GetDataCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (!e.Cancelled)
                textBlock3.Text = e.Result;
            else
                MessageBox.Show("Error with the service");
        }

        void c_getajokeCompleted(object sender, ServiceReference1.getajokeCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Cancelled != true)
                textBlock1.Text = e.Result;
            else
                MessageBox.Show("Error with service");
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            c.getajokeAsync();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int o;
            if (int.TryParse(textBox_hop.Text, out o))
            {
                c.GetDataAsync(o);
            }
            else
            {
                MessageBox.Show("enter an integer value");
                textBox_hop.Text = string.Empty;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int o;
            if (!string.IsNullOrEmpty(textBox_multihopnumber.Text))
            {
                c.GetDataUsingDataContractAsync(new ServiceReference1.CompositeType(){BoolValue=(bool)checkBox1.IsChecked,StringValue=textBox_multihopnumber.Text});
            }
            else
            {
                MessageBox.Show("Enter Text into the text box");
            }
        }
    }
}