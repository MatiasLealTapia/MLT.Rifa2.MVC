using Microsoft.AspNetCore.Authentication.Cookies;
using MLT.Rifa2.MVC.DTOs;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace MLT.Rifa2.MVC.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> PostOrganizationForm(OrganizationFormViewModel model)
        {
            string apiUrl = $"PostOrganizationForm";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var objAdd = new OrganizationFormDTO()
                {
                    OrganizationName = model.OrganizationName,
                    OrganizationEmail = model.OrganizationEmail,
                    OrganizationPhoneNumber = model.OrganizationPhoneNumber,
                    OrganizationFormInformation = model.OrganizationFormInformation,
                    OrganizationTypeId = model.OrganizationTypeId,
                };
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(objAdd), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Contains("T"))
                    {
                        return true;
                    }
                    else if (content.Contains("F"))
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Integración - Intente mas tarde.");
            }
        }
    }
}
