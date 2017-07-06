using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.IO;
using MediaStoreApi.Domain.Core;


namespace MediaStoreApi.Extensions
{
    public class MediaFileContentResult : IHttpActionResult
    {
        private Stream Stream { get; set; }
        private string MediaType { get; set; }
        public MediaFileContentResult(FileModel model)
        {
            Stream = model.Stream;
            MediaType = model.MediaType;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = new StreamContent(Stream);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaType);
            return Task.FromResult(response);
        }
    }
}