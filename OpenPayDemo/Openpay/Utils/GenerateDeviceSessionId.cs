using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sysne.Core.ApiClient;
using Core.Lib.ApiClient;
using System.Net.Http;

namespace Openpay.Utils {
    public static class GenerateDeviceSessionId {

        private static string result = "";
      

        public static async Task<string> GetDeviceSessionIdAsync(string baseUrl, string merchantId, string sessionId) {
            string url = $"{baseUrl}/oa/logo.htm?m={merchantId}&s={sessionId}";
            PaymentsAPI api = new PaymentsAPI(url);

            var (statusCode, contenido) = await api.GetSessionId<HttpContent>(url);


            Console.WriteLine(contenido.Headers.Contains("SESSION"));

            return result;
        }

    }
}
