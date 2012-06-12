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
using Microsoft.Phone.Tasks;

namespace PhoneFeatureDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "PV Sucks :P";
            emailComposeTask.Body = "honest to god , he might have high IQ and knows Kung Fu";
            emailComposeTask.To = "b-radsan@microsoft.com";
            emailComposeTask.Cc = "cc@example.com";
            emailComposeTask.Bcc = "bcc@example.com";

            emailComposeTask.Show();

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();

            phoneCallTask.PhoneNumber = "800-HIGHIQ";
            phoneCallTask.DisplayName = "muhahahahhahahaha";

            phoneCallTask.Show();

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask();

            smsComposeTask.To = "800-GOD";
            smsComposeTask.Body = "PV sucks";

            smsComposeTask.Show();

        }
    }
}