using System;
using System.Collections.Generic;
using MediaStoreApi.Domain.Core;
using MediaStoreApi.Domain.Interfaces;
using MediaStoreApi.Services.Interfaces;
using MediaStoreApi.Extension.MediaFileOperations;
using MediaStoreApi.Domain.Exceptions;
using System.IO;



namespace MediaStoreApi.Infrastructure.Business
{
    public class MediaStoreService : IMediaService
    {
        private IMediaInfoProvider _mediaInfoProvider;
        private IMediaFileProvider _mediaFileProvider;
        private IFolderManager _folderManager;
        private IMediaFileOperationsSwitch _operations;
        private Object locked = new Object();


        public MediaStoreService(IMediaInfoProvider mediaInfoProvider,
                                 IMediaFileProvider mediaFileProvider,
                                 IFolderManager manager,
                                 IMediaFileOperationsSwitch operations)
        {
            _folderManager = manager;
            _mediaFileProvider = mediaFileProvider;
            _mediaInfoProvider = mediaInfoProvider;
            _operations = operations;
           
        }
        public void Delete(FileModel model)
        {
            lock (locked)
            {
                //delete info into storeInfo
                var item = _mediaInfoProvider.Delete(model);
                //delete orginal 
                _mediaFileProvider.Delete(_folderManager.MediaFilePathGenerate(item));
                //delete miniature
                item.IsMiniature = true;
                _mediaFileProvider.Delete(_folderManager.MediaFilePathGenerate(item));

                _mediaInfoProvider.Save();
            }
           
        }

        public FileModel Get(FileModel model)
        {
            lock (locked)
            {
                var item = _mediaInfoProvider.Get(model);
                item.IsMiniature = model.IsMiniature;
                item.Stream = new MemoryStream( _mediaFileProvider.GetByte(_folderManager.MediaFilePathGenerate(item)));
                return item;
            }
        }
        //Resizable
        public FileModel Get(FileModel model, int height, int width)
        {
            lock (locked)
            {
                var item = _mediaInfoProvider.Get(model);
                //checking provider for miniature creation
                var miniProvider = _operations[item.MediaType];
               if (miniProvider == null)
                    throw new InvalidMediaFileTypeException("provider for that media type  is not exist");

                if (!(miniProvider is IMediaFileDynamicResizable) )
                    throw new InvalidMediaFileTypeException("the media file doesn't support dynamic resize");

                
                item.Content = _mediaFileProvider.GetByte(_folderManager.MediaFilePathGenerate(item));

                item.Stream = (miniProvider as IMediaFileDynamicResizable).DynamicResizeImage(item.Content, height, width);
                return item;
            }
        }
        public IEnumerable<FileModel> List()
        {
            return _mediaInfoProvider.List();
        }

        public FileModel Set(FileModel model)
        {
            lock (locked)
            {
                //checking provider for miniature creation
                var miniProvider = _operations[model.MediaType];
                 
                //save file info
                model.MiniatureFolderName = miniProvider != null ? _folderManager.ThumbnailFolderName : string.Empty;
                var saveModel = _mediaInfoProvider.Set(model);

                //save original file
                _mediaFileProvider.Set(model.Content, _folderManager.MediaFilePathGenerate(saveModel));
                
                //if miniprovider exist - save file miniature
                if (miniProvider != null)
                {
                    saveModel.IsMiniature = true;
                    _mediaFileProvider.Set(miniProvider.GetMiniature(model.Content), _folderManager.MediaFilePathGenerate(saveModel));
                }
                
                   
                _mediaInfoProvider.Save();
                return saveModel;
            }
        }

        
        public void Update(FileModel model)
        {
            throw new NotImplementedException();
        }
       
    }
}
