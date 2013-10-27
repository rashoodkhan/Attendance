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
using System.IO.IsolatedStorage;
using System.Windows.Shapes;

namespace Attendance
{
    public class absnt_item
    {
        public Grid item;
        private TextBlock txt_box;
        int roll_num;
        attend_end main;

        Rectangle back;

        private void expand(object sender, RoutedEventArgs e)
        {
            tk_attend.temp_attnd_record[roll_num] = true;
            tk_attend.temp_absnt.Remove(roll_num);
            txt_box.Visibility = Visibility.Collapsed;
            back.Visibility = Visibility.Collapsed;
        }

        public absnt_item(attend_end m, int i, int roll_num)
        {
            main = m;
            item = new Grid();
            this.roll_num = roll_num;

            item.Height = 100;
            item.Width = 100;
            item.HorizontalAlignment = HorizontalAlignment.Left;
            item.VerticalAlignment = VerticalAlignment.Top;
            int l, t;
            l = 10 + 120 * (i % 4);
            t = 10 + 120 * (i / 4);
            item.Margin = new Thickness(l,t,0,0);

            back = new Rectangle();
            item.Children.Add(back);

            back.Height = back.Width = 100;
            back.Margin = new Thickness(0, 0, 0, 0);
            back.MouseLeftButtonDown += new MouseButtonEventHandler(expand);
            back.Fill = new SolidColorBrush(Colors.White);

            txt_box = new TextBlock();

            item.Children.Add(txt_box);

            txt_box.Height = 56;
            txt_box.Width = 100;
            txt_box.TextWrapping = TextWrapping.NoWrap;
            txt_box.TextAlignment = TextAlignment.Center;
            txt_box.FontSize = 40;
            txt_box.Margin = new Thickness(0, 22, 0, 0);
            txt_box.Foreground = new SolidColorBrush(Colors.Black);
            txt_box.HorizontalAlignment = HorizontalAlignment.Left;
            txt_box.VerticalAlignment = VerticalAlignment.Top;
            txt_box.MouseLeftButtonDown += new MouseButtonEventHandler(expand);

            txt_box.Text = Convert.ToString(roll_num);

        }
    }

    public partial class attend_end : PhoneApplicationPage
    {
        public IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public attend_end()
        {
            InitializeComponent();

            reset_list();

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

            int ind = tk_attend.temp_absnt.Count;
            absnt_item obj;

            for (int i = 0; i < ind; i++)
            {
                obj = new absnt_item(this, i, tk_attend.temp_absnt[i]);
                this.absnt_disp.Children.Add(obj.item);
            }
        }

        private void save(object sender, RoutedEventArgs e)
        {
            Batch batch = (Batch)storage[App.batch_name];

            batch.date_list.Add(DateTime.Now);
            batch.lect_num++;

            List<Student> student_list = (List<Student>)storage[App.batch_name + "student"];

            for (int i = 1; i <= batch.num_students; i++)
            {
                student_list[i].attended.Add(tk_attend.temp_attnd_record[i]);
            }

            storage[App.batch_name] = batch;
            storage[App.batch_name + "student"] = student_list;

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}