using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaStoreApi.Domain.Interfaces
{
    public interface IMediaFileProvider
    {
        FileStream GetStream(string path);
        byte[] GetByte(string path);
        void Delete(string path);
        void Set(byte[] content, string path);
    }
}
