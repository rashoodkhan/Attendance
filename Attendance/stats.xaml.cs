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
using System.Diagnostics;

namespace Attendance
{
    public partial class stats : PhoneApplicationPage
    {
        public IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
        Batch batch;
        List<Student> student_list;

        public stats()
        {
            InitializeComponent();

            batch = (Batch)storage[App.batch_name];
            student_list = (List<Student>)storage[App.batch_name+"student"];

            //for (int i = 1; i < student_list.Count; i++)
            //{

            //    for (int j = 0; j < student_list[i].attended.Count; j++)
            //    {
            //        if (student_list[i].attended[j] == true)
            //            def_disp.Text += Convert.ToString(i) + "   ";
            //    }
            //    def_disp.Text += "\n";

            //}

            init();
            
        }

        public void init()
        {
            id.Text = batch.course_id;
            name.Text = batch.name;
            num.Text = batch.lect_num.ToString();
        }

        private void filter_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            String def_list = "";
            int total = batch.lect_num;
            int count;

            for (int i = 1; i < student_list.Count; i++)
            {
                count = 0;
                for (int j = 0; j < student_list[i].attended.Count; j++)
                {
                    if (student_list[i].attended[j] == true)
                        count++;
                }
                if (list.SelectedItem == less)
                    Debug.WriteLine("hello");

                if (list.SelectedItem == less)
                {
                    if (Convert.ToDouble(count) / Convert.ToDouble(total) < Convert.ToDouble(val.Text) / 100)
                    {
                        def_list += Convert.ToString(i) + ",   ";
                    }
                }
                else if (list.SelectedItem == more)
                {
                    if (Convert.ToDouble(count) / Convert.ToDouble(total) > Convert.ToDouble(val.Text) / 100)
                    {
                        def_list += Convert.ToString(i) + ",   ";
                    }
                }
                else if (list.SelectedItem == equal)
                {
                    if (Convert.ToDouble(count) / Convert.ToDouble(total) == Convert.ToDouble(val.Text) / 100)
                    {
                        def_list += Convert.ToString(i) + ",   ";
                    }
                }
            }
            def_disp.Text = def_list;

        }

    }
}