using Sysne.Core.ApiClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Core.Lib.ApiClient {
    class PaymentsAPI : WebApiClient {
        public PaymentsAPI(string url) : base(url) { }


        public async Task<(HttpStatusCode StatusCode, TResponse Content)> GetSessionId<TResponse>(string url) =>
            await CallGetAsync<TResponse>(url);



    }
}
