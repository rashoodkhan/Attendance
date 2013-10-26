﻿using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Attendance
{
    [DataContract]
    public class Batch
    {
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public int num_students { get; set; }
        [DataMember]
        public int lect_num { get; set; }
        [DataMember]
        public List<DateTime> date_list { get; set; }

        public Batch(String name, int num)
        {
            this.name = name;
            this.num_students = num;

            List<Student> student_list = new List<Student>();
            String st_name = name+"student";

            App.storage.Add(st_name, student_list);
        }
    }
}
