using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Openpay.Xamarin;
using Openpay.Xamarin.Abstractions;
using Openpay;

namespace Core.Lib.BL {
    public class PagosBL {

        public PagosBL() {

        }

        public async Task<Token> CrearTarjeta(Card card) {
            Token token = new Token();
            if (CrossOpenpay.IsSupported) {
                token =  await CrossOpenpay.Current.CreateTokenFromCard(card);
            }
            return token;
        }

        public async Task<string> CreateDeviceSessionId() {
            string deviceSessionId = string.Empty;
            if (CrossOpenpay.IsSupported) {
                deviceSessionId = await CrossOpenpay.Current.CreateDeviceSessionId();
            }
            return deviceSessionId;
        }
    }
}
