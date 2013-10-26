using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Windows.Input;
using System.Windows.Media;

namespace Attendance
{
    public class d_item
    {
        public Grid item;
        private TextBlock txt_box;
        int roll;
        tk_attend main;
        bool isPresent;

        Rectangle back;

        private void expand(object sender, RoutedEventArgs e)
        {
            if (isPresent)
            {
                main.temp_prsnt.Remove(roll);
                tk_attend.temp_absnt.Add(roll);
                main.reset_p_list();
                main.reset_a_list();
            }
            else
            {
                main.temp_prsnt.Add(roll);
                tk_attend.temp_absnt.Remove(roll);
                main.reset_p_list();
                main.reset_a_list();
            }

            txt_box.Visibility = Visibility.Collapsed;
            back.Visibility = Visibility.Collapsed;
        }

        public d_item(tk_attend m, int i, int roll_num, bool temp)
        {
            isPresent = temp;
            main = m;
            roll = roll_num;
            item = new Grid();

            item.Height = 60;
            item.Width = 60;
            item.HorizontalAlignment = HorizontalAlignment.Right;
            item.VerticalAlignment = VerticalAlignment.Top;
            int r;
            r = 70*i;
            item.Margin = new Thickness(0,0,r,0);

            back = new Rectangle();
            item.Children.Add(back);

            back.Height = back.Width = 60;
            back.Margin = new Thickness(0, 0, 0, 0);
            back.MouseLeftButtonDown += new MouseButtonEventHandler(expand);
            back.Fill = new SolidColorBrush(Colors.White);

            txt_box = new TextBlock();

            item.Children.Add(txt_box);

            txt_box.Height = 60;
            txt_box.Width = 60;
            txt_box.TextWrapping = TextWrapping.NoWrap;
            txt_box.TextAlignment = TextAlignment.Center;
            txt_box.FontSize = 40;
            txt_box.Margin = new Thickness(0, 0, 0, 0);
            txt_box.Foreground = new SolidColorBrush(Colors.Black);
            txt_box.HorizontalAlignment = HorizontalAlignment.Left;
            txt_box.VerticalAlignment = VerticalAlignment.Top;
            txt_box.MouseLeftButtonDown += new MouseButtonEventHandler(expand);

            txt_box.Text = Convert.ToString(roll_num);

        }
    }
    public partial class tk_attend : PhoneApplicationPage
    {
        public IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        int cur_num;
        public static bool[] temp_attnd_record;
        public static List<int> temp_absnt;
        public List<int> temp_prsnt;
        Batch batch;

        public tk_attend()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            cur_num = 1;
            roll_box.Text = Convert.ToString(cur_num);
            batch = (Batch)storage[App.batch_name];

            temp_attnd_record = new bool[batch.num_students + 1];
            for (int i = 0; i < batch.num_students; i++)
            {
                temp_attnd_record[i] = false;
            }

            temp_absnt = new List<int>();
            temp_prsnt = new List<int>();
        }

        public void reset_p_list()
        {
            p_past_disp.Children.Clear();

            int ind = temp_prsnt.Count;
            d_item obj;
            int c = 0;

            for (int i = ind - 1; i >= 0; i--)
            {
                obj = new d_item(this, c, temp_prsnt[i],true);
                this.p_past_disp.Children.Add(obj.item);
                c++;
            }

        }

        public void reset_a_list()
        {
            a_past_disp.Children.Clear();

            int ind = temp_absnt.Count;
            d_item obj;

            int c = 0;
            for (int i = ind - 1; i >= 0; i--)
            {
                obj = new d_item(this, c, temp_absnt[i],false);
                this.a_past_disp.Children.Add(obj.item);
                c++;
            }
        }

        private void present_click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            temp_prsnt.Add(cur_num);
            temp_attnd_record[cur_num] = true;
            inc_roll();
            reset_p_list();
        }

        private void absent_click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            temp_absnt.Add(cur_num);
            inc_roll();
            reset_a_list();
        }

        public void inc_roll()
        {
            cur_num++;

            if (cur_num > batch.num_students)
                NavigationService.Navigate(new Uri("/attend_end.xaml", UriKind.Relative));
            roll_box.Text = Convert.ToString(cur_num);
        }

        private void cancel_(object sender, System.EventArgs e)
        {
        	NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}