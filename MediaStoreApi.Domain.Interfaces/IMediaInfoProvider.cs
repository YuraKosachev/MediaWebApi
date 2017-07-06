using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Core;

namespace MediaStoreApi.Domain.Interfaces
{
    public interface IMediaInfoProvider
    {
        IEnumerable<FileModel> List();
        FileModel Get(FileModel model);
        FileModel Set(FileModel model);
        void Update(FileModel model);
        FileModel Delete(FileModel model);
        void Save();
    }
}
