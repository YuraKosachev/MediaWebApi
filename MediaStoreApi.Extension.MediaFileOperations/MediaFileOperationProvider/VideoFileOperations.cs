using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Interfaces;

namespace MediaStoreApi.Extension.MediaFileOperations
{
    public class VideoFileOperations : IMediaFileOperations
    {
        public byte[] GetMiniature(byte[] content)
        {
            throw new NotImplementedException();
        }
    }
}
