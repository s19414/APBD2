using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD2
{
    internal class IncompleteRecordError : Exception
    {
        public IncompleteRecordError(string message) : base(message) { }
    }
}
