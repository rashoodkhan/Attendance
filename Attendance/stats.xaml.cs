using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using System.Windows.Input;

namespace Attendance
{
    public class btch_item
    {
        public Grid item;
        private TextBlock name;
        private String btch_name;
        stats main;

        private void expand(object sender, RoutedEventArgs e)
        {
            App.batch_name = btch_name;
            App.nameIn = true;
            main.NavigationService.Navigate(new Uri("/attend.xaml", UriKind.Relative));
        }

        public btch_item(stats m, int i, String btch_name)
        {
            main = m;
            item = new Grid();
            this.btch_name = btch_name;

            item.Height = 120;
            item.Width = 460;
            item.HorizontalAlignment = HorizontalAlignment.Left;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Margin = new Thickness(10, 10 + i * 120, 0, 0);

            name = new TextBlock();

            item.Children.Add(name);

            name.Height = 38;
            name.Width = 350;
            name.TextWrapping = TextWrapping.NoWrap;
            name.FontSize = 30;
            name.Margin = new Thickness(0, 0, 0, 0);
            name.Foreground = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            name.HorizontalAlignment = HorizontalAlignment.Left;
            name.VerticalAlignment = VerticalAlignment.Top;
            name.MouseLeftButtonDown += new MouseButtonEventHandler(expand);

            name.Text = btch_name;

        }
    }
    public partial class stats : PhoneApplicationPage
    {
        public IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public stats()
        {
            InitializeComponent();
            reset_list();
        }

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
            btch_item obj;

            for (int i = 0; i < ind; i++)
            {
                obj = new btch_item(this, i, App.batch_name_list[i]);
                this.batch_disp.Children.Add(obj.item);
            }
        }

    }
}