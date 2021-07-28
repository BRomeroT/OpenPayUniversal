using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Core.Lib.BL;
using Sysne.Core.MVVM;
using Openpay.Xamarin;
using Openpay.Xamarin.Abstractions;
using Openpay;
using Openpay.Entities.Request;
using Openpay.Entities;
using Openpay.Utils;
using Openpay.Utils;

namespace Core.Lib.ViewModels {
    public class PagoViewModel : ViewModelBase{
        private const string MerchantId = "m35dwwpbgz53p5s34joz";
        private const string ApiKey = "pk_1089415eb8fc4395bf297c5fdcb8c5f0";
        private const string PvKey = "sk_0cfe36c2931347cb91e0567c1e8ddcc0";
        private const string baseUrl = "https://sandbox-api.openpay.mx";



        private string holderName;
        [Required(ErrorMessage = "Falta el Nombre del tarjetahabiente")]
        public string HolderName { get => holderName; set => Set(ref holderName, value); }
        private string number;
        [Required(ErrorMessage = "Falta el Numero de la tarjeta")]
        public string Number { get => number; set => Set(ref number, value); }
        private string expirationMonth;
        [Required(ErrorMessage = "Falta el Mes de expiración")]
        public string ExpirationMonth { get => expirationMonth; set => Set(ref expirationMonth, value); }
        private string expirationYear;
        [Required(ErrorMessage = "Falta el Año de expiración")]
        public string ExpirationYear { get => expirationYear; set => Set(ref expirationYear, value); }
        private int cvv2;
        [Required(ErrorMessage = "Falta el CVV")]
        public int Cvv2 { get => cvv2; set => Set(ref cvv2, value); }

        private int cantidad;
        public int Cantidad { get => cantidad; set => Set(ref cantidad, value); }
        private string message;
        public string Message { get => message; set => Set(ref message, value); }

        public PagoViewModel() {
            HolderName = "Francisco Pantera";
            Number = "4111111111111111";
            ExpirationMonth = "12";
            ExpirationYear = "21";
            Cvv2 = 132;
            Cantidad = 2500;
        }
        RelayCommand pagarCommand = null;
        public RelayCommand PagarCommand {
            get => pagarCommand ??= new RelayCommand(async () =>
            {
                var card = new Openpay.Xamarin.Abstractions.Card
                {
                    HolderName = HolderName,
                    Number = Number,
                    ExpirationMonth = ExpirationMonth,
                    ExpirationYear = ExpirationYear,
                    Cvv2 = Cvv2
                };
                PagosBL pagosBL = new PagosBL();

                OpenpayAPI openpayAPI = new OpenpayAPI(PvKey, MerchantId);
                openpayAPI.Production = false;

                var token = await pagosBL.CrearTarjeta(card);


                // var sessionId = await pagosBL.CreateDeviceSessionId();
                //var sessionId = "A48D4D146FF598C02605FC81C6083B9E";
                var sessionId = await GenerateDeviceSessionId.GetDeviceSessionIdAsync(baseUrl, MerchantId, "12345678910");

                ChargeRequest request = new ChargeRequest();
                Customer customer = new Customer();
                customer.Name = "Juan";
                customer.LastName = "Vazquez Juarez";
                customer.PhoneNumber = "4423456723";
                customer.Email = "juan.vazquez@em.com.mx";

                request.Method = "card";
                request.SourceId = token.Id;
                request.Cvv2 = ""+Cvv2;
                request.Amount = new Decimal(Cantidad);
                request.Currency = "MXN";
                request.Description = "Cargo inicial a mi merchant";
                request.OrderId = "oid-0005555";
                request.DeviceSessionId = sessionId;
                request.Customer = customer;

                Charge charge = openpayAPI.ChargeService.Create(request);

               
                Message = $"Token id: {token.Id} - DeviceSessionId: {sessionId}";
            }, () => Validate(this, false)
            , dependencies: (this, new[] { nameof(HolderName), nameof(Number), nameof(ExpirationMonth), nameof(ExpirationYear), nameof(Cvv2) }));
        }

        

    }
}
