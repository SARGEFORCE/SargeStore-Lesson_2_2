using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace SargeStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _Client;
        protected readonly string _ServiceAddress;
        protected BaseClient(IConfiguration config, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;

            _Client = new HttpClient
            {
                BaseAddress = new Uri(config["ClientAddress"])
            };

            var headers = _Client.DefaultRequestHeaders.Accept;
            headers.Clear();
            headers.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public void Dispose() => Dispose(true);

        private bool _Disposed;
        protected virtual void  Dispose(bool Disposing)
        {
            _Disposed = true;
            if (!Disposing || _Disposed) return;

            _Client.Dispose();
        }
    }
}
