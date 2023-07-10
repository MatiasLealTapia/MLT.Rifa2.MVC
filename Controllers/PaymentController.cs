using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace MLT.Rifa2.MVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string apiKey = Environment.GetEnvironmentVariable("API_KEY");
        private readonly string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");

        public IActionResult CreatePayment()
        {
            var parameters = new Dictionary<string, string>
                            {
                                { "apiKey", apiKey },
                                { "commerceOrder", "71493" },
                                { "subject", "Descripción de la orden" },
                                { "currency", "CLP" },
                                { "amount", "5000" },
                                { "email", "matiasa.lealt.23@gmail.com" },
                                { "urlConfirmation", "https://localhost:7017/Payment/PaymentConfirmation" },
                                { "urlReturn", "https://localhost:7017/Payment/PaymentConfirmation" },
                            };

            // Genera la firma
            string signature = GenerateSignature(parameters, secretKey);

            // Agrega la firma a los parámetros
            parameters.Add("s", signature);

            // Construye la URL del servicio a consumir
            string baseUrl = "https://sandbox.flow.cl/api";
            string endpoint = "/payment/create";
            string url = baseUrl + endpoint;

            // Crea el cliente RestSharp
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            // Agrega los parámetros al cuerpo de la solicitud
            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            // Realiza la llamada POST utilizando RestSharp
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseObject = JObject.Parse(response.Content);

                // Obtiene la URL de redirección y el token desde la respuesta
                string paymentUrl = responseObject["url"].ToString();
                string token = responseObject["token"].ToString();

                // Construye la URL de redirección
                string redirectUrl = paymentUrl + "?token=" + token;

                // Redirige al usuario a la página de pago de Flow
                return Redirect(redirectUrl);
            }
            else
            {
                // Procesa el error

                // Redirige al usuario a una página de error en tu sitio web
                return RedirectToAction("PaymentError");
            }
        }

        public IActionResult GetPaymentStatus(string paymentId)
        {
            // Construye la URL del servicio a consumir
            string baseUrl = "https://sandbox.flow.cl/api";
            string endpoint = "/payment/getStatus";
            string url = baseUrl + endpoint;

            // Crea el cliente RestSharp
            var client = new RestClient(url);

            // Agrega los parámetros necesarios para obtener el estado del pago
            var parameters = new Dictionary<string, string>
            {
                { "paymentId", paymentId },
                { "apiKey", apiKey }
            };

            // Genera la firma
            string signature = GenerateSignature(parameters, secretKey);

            // Agrega la firma a los parámetros
            parameters.Add("s", signature);

            // Codifica los parámetros en formato URL y los agrega a la URL
            var queryParameters = string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
            string fullUrl = url + "?" + queryParameters;

            // Realiza la llamada GET utilizando RestSharp
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Procesa la respuesta exitosa
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        private string GenerateSignature(Dictionary<string, string> parameters, string secretKey)
        {
            var sortedParameters = parameters.OrderBy(p => p.Key);

            StringBuilder toSign = new StringBuilder();

            foreach (var parameter in sortedParameters)
            {
                toSign.Append(parameter.Key);
                toSign.Append(parameter.Value);
            }

            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] toSignBytes = Encoding.UTF8.GetBytes(toSign.ToString());

            using (var hmac = new HMACSHA256(secretKeyBytes))
            {
                byte[] signatureBytes = hmac.ComputeHash(toSignBytes);
                string signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();
                return signature;
            }
        }

        public IActionResult PaymentConfirmation()
        {
            return View();
        }

        public IActionResult PaymentError()
        {
            // Aquí puedes mostrar una página de error de pago
            return View();
        }
    }
}