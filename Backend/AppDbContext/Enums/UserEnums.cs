using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDbContext.Enums
{
    public enum Discriminator
    {
        Teacher = 0,
        Student
    }
  
    public enum LoginEnumResult
    {
        Success = 1,
        Failure,
        Lockout,
    }
}
