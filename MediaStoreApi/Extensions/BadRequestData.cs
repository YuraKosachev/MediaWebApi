using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MediaStoreApi.Domain.Exceptions;

namespace MediaStoreApi.Extensions
{
    public class BadRequestData : IHttpActionResult
    {
        private Exception Ex { get; set; }
        public BadRequestData(Exception ex)
        {
            Ex = ex;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            response.Content = new ObjectContent<Exception>(Ex, new JsonMediaTypeFormatter());
            return Task.FromResult(response);
        }
    }
}