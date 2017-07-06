using System;
using MediaStoreApi.Domain.Core;

namespace MediaStoreApi.Domain.Interfaces
{
    public interface IFolderManager
    {
        string MediaFolderPath { get; }
        string ThumbnailFolderName { get; }
        void CreateFolder(string path);
        void CreateFile(string path, Action create);
        string MediaFilePathGenerate(FileModel model);
        string PathGenerator(params string[] paths);
    }
}
