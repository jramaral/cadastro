using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Cadastro.UI.Models;
using Newtonsoft.Json;

namespace Cadastro.UI.Validations
{
    public class RecaptchaValidation
    {
        internal static async Task<bool> Validate(HttpRequestBase request)
        {
            var recaptchaResponse = request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(recaptchaResponse)) return false;


            using (var client = new HttpClient { BaseAddress = new Uri("https://www.google.com") })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret",
                        WebConfigurationManager.AppSettings["recaptchaPrivateKey"]),
                    new KeyValuePair<string, string>("response", recaptchaResponse),
                    new KeyValuePair<string, string>("remoteip", request.UserHostAddress)
                });

                var result = await client.PostAsync("/recaptcha/api/siteverify", content);

                result.EnsureSuccessStatusCode();

                var jsonString = await result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<CaptchaResponse>(jsonString);

                return response.Success;
            }
        }
    }
}