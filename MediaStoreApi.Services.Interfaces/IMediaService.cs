using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Core;

namespace MediaStoreApi.Services.Interfaces
{
    public interface IMediaService
    {
        IEnumerable<FileModel> List();
        FileModel Set(FileModel model);
        FileModel Get(FileModel model);
        FileModel Get(FileModel model, int height, int width);
        void Delete(FileModel model);
        void Update(FileModel model);
    }
}
