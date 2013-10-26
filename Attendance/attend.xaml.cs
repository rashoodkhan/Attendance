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
using System.Windows.Threading;

namespace Attendance
{
    public partial class attend : PhoneApplicationPage
    {
        bool isStart;
        Batch batch;
        DispatcherTimer timer;
        int cur_num;
        public static bool[] temp_attnd_record;
        public static List<int> temp_absnt;
        public IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public attend()
        {
            InitializeComponent();

            isStart = false;
            batch = (Batch)storage[App.batch_name];

            header.Text = "class :: " + batch.name;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);

            cur_num = 1;

            temp_attnd_record = new bool[batch.num_students+1];
            for (int i = 0; i < batch.num_students; i++)
            {
                temp_attnd_record[i] = true;
            }

            temp_absnt = new List<int>();
        }

        private void main_toggle(object sender, RoutedEventArgs e)
        {
            if (isStart == false)
            {
                isStart = true;
                timer.Start();
                main_button.Content = "Pause";
            }
            else if (isStart == true)
            {
                isStart = false;
                timer.Stop();
                main_button.Content = "Resume";
            }
        }

        private void attend_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            temp_attnd_record[cur_num] = false;
            temp_absnt.Add(cur_num);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            cur_num++;

            if (cur_num > batch.num_students)
            {
                timer.Stop();
                NavigationService.Navigate(new Uri("/attend_end.xaml", UriKind.Relative));
            }
            roll_num.Text = Convert.ToString(cur_num);
        }
    }
}