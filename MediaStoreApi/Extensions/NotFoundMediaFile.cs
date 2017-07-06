using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace MediaStoreApi.Extensions
{
    public class NotFoundMediaFile : IHttpActionResult
    {
        private Exception Ex{ get; set; }
        public NotFoundMediaFile(Exception ex)
        {
            Ex = ex;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            response.Content = new ObjectContent<Exception>(Ex, new JsonMediaTypeFormatter());
            return Task.FromResult(response);
        }
    }
}