using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginClass
{
    public record User (string username,string role, DateTime latestLogin);
}
