using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Enum
{
    public enum Category
    {
        [Description("Exam")]
        Exam,
        [Description("Holiday")]
        Holiday,
        [Description("Events")]
        Events,
        [Description("Fee")]
        Fee,
        [Description("Other")]
        Other
    }
}
