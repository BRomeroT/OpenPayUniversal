using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Openpay.Xamarin;
using Openpay;


namespace OpenPayDemo {
    public partial class App : Application {
        private const string MerchantId = "m35dwwpbgz53p5s34joz";
        private const string ApiKey = "pk_1089415eb8fc4395bf297c5fdcb8c5f0";
        private const string PvKey = "sk_0cfe36c2931347cb91e0567c1e8ddcc0";
        private const string baseUrl = "https://sandbox-api.openpay.mx";
        public App() {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() {

            base.OnStart();
            //Sandbox
            OpenpayAPI openpayAPI = new OpenpayAPI(PvKey, MerchantId);
            openpayAPI.Production = false;

            // Initialize Openpay
            if (CrossOpenpay.IsSupported) {
                CrossOpenpay.Current.Initialize(MerchantId, ApiKey, false);
            }

            //var sessionId = UUID.RandomUUID().ToString();
            //sessionId = sessionId.Replace("-", string.Empty);

            //using (var browser = new WebView) {
            //    var url = $"{baseUrl}/oa/logo.htm?m={MerchantId}&s={sessionId}";
            //    webView.LoadUrl(url);
            //};


           


        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
