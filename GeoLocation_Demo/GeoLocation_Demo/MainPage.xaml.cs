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
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using System.IO.IsolatedStorage;
using System.IO;

namespace GeoLocation_Demo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            GeoCoordinateWatcher g = new GeoCoordinateWatcher();
            g.MovementThreshold = 10;
            g.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(g_PositionChanged);
            g.Start();

            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("myFile.txt", FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new StreamReader(fileStream))
            {    //Visualize the text data in a TextBlock text    
                
            }
        }

        void g_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            //hrow new NotImplementedException();
            textBlock1.Text = e.Position.Location.Latitude.ToString();
            map1.Center = e.Position.Location;

            Pushpin p = new Pushpin();
            p.Location = e.Position.Location;

            map1.Children.Add(p);
        }
    }
}