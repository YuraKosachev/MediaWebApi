using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Interfaces;

namespace MediaStoreApi.Infrastructure.FileManage
{
    public class MediaStoreFileProvider : IMediaFileProvider
    {
        public void Delete(string root)
        {
            if (!File.Exists(root))
                throw new FileNotFoundException("the required file cannot be found on disk");
            File.Delete(root);
        }

        public byte[] GetByte(string path)
        {
            using (var stream = GetStream(path))
            {
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);
                return array;
            }
        }

        public FileStream GetStream(string root)
        {
            if (!File.Exists(root))
                throw new FileNotFoundException("the required file cannot be found on disk");

            return new FileStream(root, FileMode.Open);
        }

        public void Set(byte[] content, string root)
        {
            using (FileStream fs = new FileStream(root, FileMode.Create))
            {
                fs.Write(content, 0, content.Length);
            }
        }
    }
}
