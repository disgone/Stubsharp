using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stubsharp.Common.Http;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp
{
    public class HttpClientAdapter : IHttpClient
    {
        private readonly HttpClient _http;

        public HttpClientAdapter(Func<HttpMessageHandler> handlerGenerator)
        {
            Guard.IsNotNull(handlerGenerator, nameof(handlerGenerator));

            _http = new HttpClient(handlerGenerator());
        }

        /// <summary>
        /// Sends the specified request and returns a response.
        /// </summary>
        /// <param name="request">A <see cref="IRequest"/> that represents the HTTP request</param>
        /// <param name="cancellationToken">Used to cancel the request</param>
        /// <returns>A <see cref="Task" /> of <see cref="IResponse"/></returns>
        public async Task<IResponse> Send(IRequest request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            using ( var payload = GenerateRequest(request) )
            {
                var reply = await SendRequest(payload, cancellationToken).ConfigureAwait(false);

                return await GenerateResponse(reply, cancellationToken).ConfigureAwait(false);
            }
        }

        public Task<HttpResponseMessage> SendRequest(HttpRequestMessage request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _http.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
        }

        /// <summary>
        /// Generates an <see cref="HttpRequestMessage"/> from the <see cref="IRequest"/>
        /// </summary>
        /// <param name="request">The generated <see cref="HttpRequestMessage"/></param>
        /// <returns>HttpRequestMessage.</returns>
        protected virtual HttpRequestMessage GenerateRequest(IRequest request)
        {
            Guard.IsNotNull(request, nameof(request));

            HttpRequestMessage requestMessage = null;
            try
            {
                var destination = new Uri(request.BaseAddress, request.Endpoint);
                requestMessage = new HttpRequestMessage(request.Method, destination);

                foreach (var header in request.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }

                if (request.Body is HttpContent httpContent)
                {
                    requestMessage.Content = httpContent;
                }
                else if (request.Body is string body)
                {
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, request.ContentType);
                }
                else if (request.Body is Stream bodyStream)
                {
                    requestMessage.Content = new StreamContent(bodyStream);
                    requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.ContentType);
                }
            }
            catch (Exception)
            {
                requestMessage?.Dispose();
                throw;
            }

            return requestMessage;
        }

        /// <summary>
        /// Generates the the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Task&lt;IResponse&gt;.</returns>
        protected virtual async Task<IResponse> GenerateResponse(HttpResponseMessage response, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.IsNotNull(response, nameof(response));

            string body = null;

            cancellationToken.ThrowIfCancellationRequested();

            using ( var content = response.Content )
            {
                if ( content != null )
                {
                    body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }

            return new Response(
                response.StatusCode,
                body,
                response.Headers.ToDictionary(k => k.Key, v => v.Value.First()));
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _http?.Dispose();
            }
        }
    }
}
