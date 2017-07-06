using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Core;
using MediaStoreApi.Domain.Interfaces;
using System.IO;

namespace MediaStoreApi.Infrastructure.Business
{
    public class MediaFolderManager : IFolderManager
    {
        public string MediaFolderPath { get; private set; }
        public string ThumbnailFolderName { get; private set; }
        public string HoldersFolderName { get; private set; }
        private string _unknownFileName;

        public MediaFolderManager(string mediaFolder, string thumbnailFolderName,string holderFolder,string unknownFileName)
        {
            MediaFolderPath = mediaFolder;
            ThumbnailFolderName = thumbnailFolderName;
            HoldersFolderName = holderFolder;
            _unknownFileName = unknownFileName;
            CreateFolder(PathGenerator(MediaFolderPath,holderFolder));//create (if folder is not exist) mediatype's miniature holders
            CreateFolder(CurrentFolder());//create (if folder is not exist) mediatype's miniature current folder
        }
        public void CreateFolder(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public string MediaFilePathGenerate(FileModel model)
        {
            if (model.IsMiniature)
            {
                if (string.IsNullOrEmpty(model.MiniatureFolderName))
                            return HoldersMediaFilePathGenerate(model);
                return MediaFileMiniaturePathGenerate(model);
            }
            //var mini = model.IsMiniature ? PathGenerator(model.DateOfCreate.ToString("yyyyMMdd"), ThumbnailFolderName) : model.DateOfCreate.ToString("yyyyMMdd");
            return PathGenerator(MediaFolderPath, model.DateOfCreate.ToString("yyyyMMdd"), String.Format("{0}.{1}", model.Id,model.FileExtension));
        }
        private string MediaFileMiniaturePathGenerate(FileModel model)
        {
            return PathGenerator(MediaFolderPath, model.DateOfCreate.ToString("yyyyMMdd"), model.MiniatureFolderName, String.Format("{0}.{1}", model.Id, model.FileExtension));
        }
        private string HoldersMediaFilePathGenerate(FileModel model)
        {
            var path = PathGenerator(MediaFolderPath, HoldersFolderName, String.Format("{0}.{1}", model.FileExtension, "png"));
            if (IsMediaFileExists(path))
                        return path;

            return PathGenerator(MediaFolderPath, HoldersFolderName, _unknownFileName);
            
        }
        private string CurrentFolder()
        {
            return PathGenerator(MediaFolderPath, DateTime.Now.ToString("yyyyMMdd"), ThumbnailFolderName);
        }
        public string PathGenerator(params string[] paths)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < paths.Length; i++)
            {
                if (i == 0)
                {
                    builder.Append(String.Format("{0}", paths[i]));
                    continue;
                }
                builder.Append(String.Format(@"\{0}", paths[i]));
            }
            return builder.ToString();
        }
        private bool IsMediaFileExists(string path)
        {
            return File.Exists(path);
        }
        public void CreateFile(string path,Action create)
        {
            if (!IsMediaFileExists(path))
            {
                create();
            }
        }

       
    }
}
