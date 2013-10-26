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

namespace Attendance
{
    public partial class AddBatch : PhoneApplicationPage
    {
        public AddBatch()
        {
            InitializeComponent();
        }

        private void cancel_(object sender, System.EventArgs e)
        {
            NavigationService.GoBack();
        }

        public IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        private void save(object sender, System.EventArgs e)
        {
            if (c_id.Text == "")
            {
                msg.Text = "Please enter course id";
                return;
            }

            if (name.Text == "")
            {
                msg.Text = "Please enter class name";
                return;
            }

            if (num.Text == "")
            {
                msg.Text = "Please enter class strength";
                return;
            }

            if (storage.Contains(name.Text))
            {
                msg.Text = "Class name already exists. Try another name";
                return;
            }

            Batch batch = new Batch(c_id.Text, name.Text, Convert.ToInt16(num.Text));
            storage[name.Text] = batch;

            App.batch_name_list.Add(name.Text);

            if (storage.Contains(App.batch_list_code))
            {
                storage[App.batch_list_code] = App.batch_name_list;
            }
            else
            {
                storage.Add(App.batch_list_code, App.batch_name_list);
            }

            NavigationService.GoBack();
        }
    }
}