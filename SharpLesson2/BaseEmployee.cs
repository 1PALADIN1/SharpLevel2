using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson2
{
    abstract class BaseEmployee : IComparable<BaseEmployee>
    {
        public int CompareTo(BaseEmployee other)
        {
            if (CountSalary() > other.CountSalary()) return -1;
            if (CountSalary() < other.CountSalary()) return 1;
            return 0;
        }

        public abstract double CountSalary();
    }
}
