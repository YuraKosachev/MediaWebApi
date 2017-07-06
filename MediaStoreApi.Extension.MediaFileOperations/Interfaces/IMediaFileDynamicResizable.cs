using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Interfaces;
using System.IO;

namespace MediaStoreApi.Extension.MediaFileOperations
{
    public interface IMediaFileDynamicResizable
    {
        Stream DynamicResizeImage(byte[] content, int nWidth, int nHeight);
    }
}
