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
        int roll_num { get; set; }
        [DataMember]
        List<Boolean> attended { get; set; }
    }
}
