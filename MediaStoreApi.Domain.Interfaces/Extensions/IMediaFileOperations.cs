using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStoreApi.Domain.Interfaces
{
    public interface IMediaFileOperations
    {
        byte[] GetMiniature(byte[] content);
    }
}
