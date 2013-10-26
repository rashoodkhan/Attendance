using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Attendance
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public int roll_num { get; set; }
        [DataMember]
        public List<bool> attended { get; set; }

        public Student(int roll)
        {
            roll_num = roll;
            attended = new List<bool>();
        }
    }
}
