using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDbContext.Enums
{
    public enum SessionStatusEnum
    {
        Pending = 0,
        Approved,
        Rejected
    }
    public enum PurposeEnum
    {
       Course = 0,
       Assignement,
       Exam,
       Other
    }
}
