using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStoreApi.Domain.Exceptions
{
    public class InvalidMediaFileTypeException:Exception
    {
        public InvalidMediaFileTypeException(string message) : base(message) { }
    }
}
