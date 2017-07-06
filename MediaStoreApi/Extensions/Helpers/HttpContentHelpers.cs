using System.Linq;
using MediaStoreApi.Domain.Core;
using System.Threading.Tasks;
using System.Net.Http;

namespace MediaStoreApi.Extensions
{
    public static class HttpContentHelpers
    {
        public static async Task<FileModel> GetModelAsync(this HttpContent content)
        {
            var fileModel = new FileModel();
            fileModel.Content = await content.ReadAsByteArrayAsync();
            fileModel.FileExtension = content.Headers.ContentDisposition.FileName.Trim('\"').Split('.').Last();
            fileModel.MediaType = content.Headers.ContentType.MediaType;
            return fileModel;
        }
    }
}