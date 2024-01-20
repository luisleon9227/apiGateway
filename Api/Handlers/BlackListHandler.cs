using System.Net;

namespace Api.Handlers
{
    public class BlackListHandler : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var myHeader = request.Headers.FirstOrDefault(x => x.Key == "my-header");

            if (myHeader.Value != null && myHeader.Value.Any())
            {
                return await base.SendAsync(request, cancellationToken);
            }
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.ReasonPhrase = "You are not authorized";
            return await Task.FromResult<HttpResponseMessage>(response);
        }
    }
}
