using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Exceptions
{
    public class ApiRequestException : Exception
    {
        public ApiRequestException(string msg) : base(msg) { }
    }
}
