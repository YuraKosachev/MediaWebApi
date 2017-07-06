using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MediaStoreApi.Domain.Core;
using MediaStoreApi.Domain.Interfaces;


namespace MediaStoreApi.Infrastructure.XML
{
    public class MediaStoreApiXmlProvider : IMediaInfoProvider
    {
        private XDocument _xmlRepository;
        private string _xmlFilePath;
        private IFolderManager _folderManager;
     
        public MediaStoreApiXmlProvider(IFolderManager manager,string xmlFileName)
        {
            _folderManager = manager;

            _xmlFilePath = _folderManager.PathGenerator(_folderManager.MediaFolderPath, xmlFileName);

            _folderManager.CreateFile(_xmlFilePath, () => {
                var xDoc = new XDocument();
                xDoc.Add(new XElement("files"));
                xDoc.Save(_xmlFilePath);
            });
           _xmlRepository = XDocument.Load(_xmlFilePath);
        }
        public FileModel Delete(FileModel model)
        {
           var file = _xmlRepository.GetElementById(model.Id);
           file.Remove();
           return file.MapToFileModel();
           
        }

        public FileModel Get(FileModel model)
        {
             return _xmlRepository.GetElementById(model.Id).MapToFileModel();
           
        }

        public IEnumerable<FileModel> List()
        {
            return _xmlRepository.GetElements().Select(item=> item.MapToFileModel());
        }

        public void Save()
        {
            _xmlRepository.Save(_xmlFilePath);
        }

        public FileModel Set(FileModel model)
        {
            model.Id = Guid.NewGuid();
            model.DateOfCreate = DateTime.Now;
            _xmlRepository.Root.Add(model.MapToXelement());
            return model;
        }

        public void Update(FileModel model)
        {
            throw new NotImplementedException();
        }

       
    }
}
