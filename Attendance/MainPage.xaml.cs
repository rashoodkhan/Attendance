using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Attendance.Resources;
using System.Windows.Media;
using System.Windows.Input;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;

namespace Attendance
{
    public class fav_item
    {
        public Grid item;
        private TextBlock name;
        private String btch_name;
        MainPage main;

        private void expand(object sender, RoutedEventArgs e)
        {
            App.batch_name = btch_name;
            App.nameIn = true;
            main.NavigationService.Navigate(new Uri("/tk_attend.xaml", UriKind.Relative));
        }

        public fav_item(MainPage m, int i, String btch_name)
        {
            main = m;
            item = new Grid();
            this.btch_name = btch_name;

            item.Height = 60;
            item.Width = 456;
            item.HorizontalAlignment = HorizontalAlignment.Left;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Margin = new Thickness(10, 10 + i * 90, 0, 0);

            name = new TextBlock();

            item.Children.Add(name);

            name.Height = 60;
            name.Width = 456;
            name.TextWrapping = TextWrapping.NoWrap;
            name.FontSize = 40;
            name.Margin = new Thickness(0, 0, 0, 0);
            name.Foreground = new SolidColorBrush(Colors.White);
            name.HorizontalAlignment = HorizontalAlignment.Left;
            name.VerticalAlignment = VerticalAlignment.Top;
            name.MouseLeftButtonDown += new MouseButtonEventHandler(expand);

            name.Text = btch_name;

        }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //reset_list();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        public IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public void reset_list()
        {
            //Debug.WriteLine(((List<String>)storage[App.batch_list_code]).Count);
            //Debug.WriteLine(App.storage.Count);

            batch_disp.Children.Clear();

            if (App.batch_name_list.Count == 0)
            {
                err_msg.Visibility = Visibility.Visible;
                return;
            }
            else
                err_msg.Visibility = Visibility.Collapsed;

            int ind = App.batch_name_list.Count;
            fav_item obj;

            for (int i = 0; i < ind; i++)
            {
                obj = new fav_item(this, i, App.batch_name_list[i]);
                this.batch_disp.Children.Add(obj.item);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            reset_list();
        }

        private void add_(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/addBatch.xaml", UriKind.Relative));
        }

        private void stats_(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/stat_home.xaml", UriKind.Relative));
        }
    }
}