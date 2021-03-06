﻿using System;
using System.Net;
using System.Net.Http;

namespace Simple.OData.Client
{
    /// <summary>
    /// OData client configuration settings
    /// </summary>
    public class ODataClientSettings
    {
        /// <summary>
        /// Gets or sets the OData service URL.
        /// </summary>
        /// <value>
        /// The URL address.
        /// </value>
        [Obsolete("This property is obsolete. Use BaseUri instead.")]
        public string UrlBase
        {
            get { return this.BaseUri == null ? null : this.BaseUri.AbsoluteUri; }
            set { this.BaseUri = value == null ? null : new Uri(value); }
        }

        /// <summary>
        /// Gets or sets the OData service URL.
        /// </summary>
        /// <value>
        /// The URL address.
        /// </value>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the OData client credentials.
        /// </summary>
        /// <value>
        /// The client credentials.
        /// </value>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the OData payload format.
        /// </summary>
        /// <value>
        /// The payload format (JSON or Atom).
        /// </value>
        public ODataPayloadFormat PayloadFormat { get; set; }

        /// <summary>
        /// Gets or sets the time period to wait before the request times out.
        /// </summary>
        /// <value>
        /// The timeout.
        /// </value>
        public TimeSpan RequestTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether entry properties should be extended with the resource type.
        /// </summary>
        /// <value>
        /// <c>true</c> to include resource type in entry properties; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeResourceTypeInEntryProperties { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether resource not found exception (404) should be ignored.
        /// </summary>
        /// <value>
        /// <c>true</c> to ignore resource not found exception; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreResourceNotFoundException { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether unmapped structural or navigation properties should be ignored or cause <see cref="UnresolvableObjectException"/>.
        /// </summary>
        /// <value>
        /// <c>true</c> to ignore unmapped properties; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreUnmappedProperties { get; set; }

        /// <summary>
        /// Gets or sets the OData service metadata document. If not set, service metadata is downloaded prior to the first call to the OData service and stored in an in-memory cache.
        /// </summary>
        /// <value>
        /// The content of the service metadata document.
        /// </value>
        public string MetadataDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="HttpClient"/> connection should be reused between OData requests or disposed and renewed.
        /// </summary>
        /// <value>
        /// <c>true</c> to reuse <see cref="HttpClient"/> between requests; <c>false</c> to create a new <see cref="HttpClient"/> instance for each request.
        /// </value>
        public bool ReuseHttpConnection { get; set; }

        /// <summary>
        /// Gets or sets the HttpMessageHandler factory used by HttpClient.
        /// If not set, ODataClient creates an instance of HttpClientHandler.
        /// </summary>
        /// <value>
        /// The action on <see cref="HttpMessageHandler"/>.
        /// </value>
        public Func<HttpMessageHandler> OnCreateMessageHandler { get; set; }

        /// <summary>
        /// Gets or sets the action on HttpClientHandler.
        /// </summary>
        /// <value>
        /// The action on <see cref="HttpClientHandler"/>.
        /// </value>
        public Action<HttpClientHandler> OnApplyClientHandler { get; set; }

        /// <summary>
        /// Gets or sets the action executed before the OData request.
        /// </summary>
        /// <value>
        /// The action on <see cref="HttpRequestMessage"/>.
        /// </value>
        public Action<HttpRequestMessage> BeforeRequest { get; set; }

        /// <summary>
        /// Gets or sets the action executed after the OData request.
        /// </summary>
        /// <value>
        /// The action on <see cref="HttpResponseMessage"/>.
        /// </value>
        public Action<HttpResponseMessage> AfterResponse { get; set; }

        /// <summary>
        /// Gets or sets the method that will be executed to write trace messages.
        /// </summary>
        /// <value>
        /// The trace action on <see cref="HttpResponseMessage"/>.
        /// </value>
        public Action<string, object[]> OnTrace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataClientSettings"/> class.
        /// </summary>
        public ODataClientSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataClientSettings"/> class.
        /// </summary>
        /// <param name="baseUri">The URL address.</param>
        /// <param name="credentials">The client credentials.</param>
        public ODataClientSettings(string baseUri, ICredentials credentials = null)
        {
            this.BaseUri = new Uri(baseUri);
            this.Credentials = credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataClientSettings"/> class.
        /// </summary>
        /// <param name="baseUri">The URL address.</param>
        /// <param name="credentials">The client credentials.</param>
        public ODataClientSettings(Uri baseUri, ICredentials credentials = null)
        {
            this.BaseUri = baseUri;
            this.Credentials = credentials;
        }

        internal ODataClientSettings(ISession session)
        {
            this.BaseUri = session.Settings.BaseUri;
            this.Credentials = session.Settings.Credentials;
            this.PayloadFormat = session.Settings.PayloadFormat;
            this.RequestTimeout = session.Settings.RequestTimeout;
            this.IncludeResourceTypeInEntryProperties = session.Settings.IncludeResourceTypeInEntryProperties;
            this.IgnoreResourceNotFoundException = session.Settings.IgnoreResourceNotFoundException;
            this.IgnoreUnmappedProperties = session.Settings.IgnoreUnmappedProperties;
            this.MetadataDocument = session.Settings.MetadataDocument;
            this.BeforeRequest = session.Settings.BeforeRequest;
            this.AfterResponse = session.Settings.AfterResponse;
            this.OnCreateMessageHandler = session.Settings.OnCreateMessageHandler;
            this.OnApplyClientHandler = session.Settings.OnApplyClientHandler;
        }
    }
}