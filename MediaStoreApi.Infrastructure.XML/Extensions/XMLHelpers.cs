using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MediaStoreApi.Domain.Core;
using System.IO;

namespace MediaStoreApi.Infrastructure.XML
{
    public static class XMLHelpers
    {
        public static XElement MapToXelement(this FileModel file)
        {
            var xElFile = new XElement("file");
            var xElFilelId = new XAttribute("id", file.Id);
            var xElFileDateOfCreate = new XAttribute("date", file.DateOfCreate);
            var xElFileExtension = new XAttribute("extension", file.FileExtension);
            var xElFileMediaType = new XAttribute("mediaType", file.MediaType);
            var xEFileMiniatureFolderName = new XAttribute("miniature", file.MiniatureFolderName);
            xElFile.Add(xElFilelId, xElFileDateOfCreate, xElFileExtension, xElFileMediaType, xEFileMiniatureFolderName);
            return xElFile;

        }
        public static FileModel MapToFileModel(this XElement xElement)
        {
            var file = new FileModel();
            file.Id = Guid.Parse(xElement.Attribute("id").Value);
            file.DateOfCreate = DateTime.Parse(xElement.Attribute("date").Value);
            file.FileExtension = xElement.Attribute("extension").Value;
            file.MediaType = xElement.Attribute("mediaType").Value;
            file.MiniatureFolderName = xElement.Attribute("miniature").Value;
            return file;

        }
        public static XElement GetElementById(this XDocument document,Guid id)
        {
            try
            {
                return document.Element("files").Elements("file")
                    .Where(item => Guid.Parse(item.Attribute("id").Value) == id)
                    .First();
            }
            catch (InvalidOperationException ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
        }
        public static IEnumerable<XElement> GetElements(this XDocument document)
        {
            return document.Element("files").Elements("file");
                
        }
    }
}
