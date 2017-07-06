using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaStoreApi.Domain.Core
{
    public class FileModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateOfCreate { get; set; }
        public string MediaType { get; set; }
        public string FileExtension { get; set; }
        public string MiniatureFolderName { get; set; }
        [NotMapped]
        public bool IsMiniature { get; set; }
        [NotMapped]
        public byte[] Content { get; set; }
        [NotMapped]
        public Stream Stream { get; set; }

    }
}
