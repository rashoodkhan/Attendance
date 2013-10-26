using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Input;

namespace Attendance
{
    public class absnt_item
    {
        public Grid item;
        private TextBlock txt_box;
        int roll_num;
        attend_end main;

        private void expand(object sender, RoutedEventArgs e)
        {
            attend.temp_attnd_record[roll_num] = true;
            attend.temp_absnt.Remove(roll_num);
        }

        public absnt_item(attend_end m, int i, int roll_num)
        {
            main = m;
            item = new Grid();
            this.roll_num = roll_num;

            item.Height = 120;
            item.Width = 460;
            item.HorizontalAlignment = HorizontalAlignment.Left;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Margin = new Thickness(10, 10 + i * 120, 0, 0);

            txt_box = new TextBlock();

            item.Children.Add(txt_box);

            txt_box.Height = 38;
            txt_box.Width = 350;
            txt_box.TextWrapping = TextWrapping.NoWrap;
            txt_box.FontSize = 30;
            txt_box.Margin = new Thickness(0, 0, 0, 0);
            txt_box.Foreground = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            txt_box.HorizontalAlignment = HorizontalAlignment.Left;
            txt_box.VerticalAlignment = VerticalAlignment.Top;
            txt_box.MouseLeftButtonDown += new MouseButtonEventHandler(expand);

            txt_box.Text = Convert.ToString(roll_num);

        }
    }

    public partial class attend_end : PhoneApplicationPage
    {
        public attend_end()
        {
            InitializeComponent();

        }

        public void reset_list()
        {
            //Debug.WriteLine(((List<String>)storage[App.batch_list_code]).Count);
            //Debug.WriteLine(App.storage.Count);

            absnt_disp.Children.Clear();

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
                obj = new absnt_item(this, i, App.batch_name_list[i]);
                this.batch_disp.Children.Add(obj.item);
            }
        }
    }
}