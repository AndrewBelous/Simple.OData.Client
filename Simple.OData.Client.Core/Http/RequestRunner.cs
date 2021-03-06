﻿#if DEBUG
#undef TRACE_REQUEST_CONTENT
#undef TRACE_RESPONSE_CONTENT
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.OData.Client
{
    class RequestRunner
    {
        private readonly ISession _session;

        public RequestRunner(ISession session)
        {
            _session = session;
        }

        public async Task<HttpResponseMessage> ExecuteRequestAsync(ODataRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _session.GetHttpClient();

                PreExecute(httpClient, request);

                _session.Trace("{0} request: {1}", request.Method, request.RequestMessage.RequestUri.AbsoluteUri);
#if TRACE_REQUEST_CONTENT
                    if (request.RequestMessage.Content != null)
                    {
                        var content = await request.RequestMessage.Content.ReadAsStringAsync();
                        _session.Trace("Request content:{0}{1}", Environment.NewLine, content);
                    }
#endif

                var response = await httpClient.SendAsync(request.RequestMessage, cancellationToken);
                if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();

                _session.Trace("Request completed: {0}", response.StatusCode);
#if TRACE_RESPONSE_CONTENT
                    if (response.Content != null)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _session.Trace("Response content:{0}{1}", Environment.NewLine, content);
                    }
#endif

                await PostExecute(response);
                return response;
            }
            catch (WebException ex)
            {
                throw WebRequestException.CreateFromWebException(ex);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is WebException)
                {
                    throw WebRequestException.CreateFromWebException(ex.InnerException as WebException);
                }
                else
                {
                    throw;
                }
            }
        }

        private void PreExecute(HttpClient httpClient, ODataRequest request)
        {
            if (request.Accept != null)
            {
                foreach (var accept in request.Accept)
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
                }
            }

            if (request.CheckOptimisticConcurrency &&
                (request.Method == RestVerbs.Put ||
                 request.Method == RestVerbs.Patch ||
                 request.Method == RestVerbs.Delete))
            {
                httpClient.DefaultRequestHeaders.IfMatch.Add(EntityTagHeaderValue.Any);
            }

            foreach (var header in request.Headers)
            {
                request.RequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (_session.Settings.BeforeRequest != null)
                _session.Settings.BeforeRequest(request.RequestMessage);
        }

        private async Task PostExecute(HttpResponseMessage responseMessage)
        {
            if (_session.Settings.AfterResponse != null)
                _session.Settings.AfterResponse(responseMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw await WebRequestException.CreateFromResponseMessageAsync(responseMessage);
            }
        }
    }
}
