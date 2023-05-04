using MLT.Rifa2.MVC.DTOs;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MLT.Rifa2.MVC.Services
{
    public class ReferentService : IReferentService
    {
        private HttpClient _httpClient;
        private readonly ILogger<OrganizationDTO> _logger;

        public ReferentService(HttpClient httpClient, ILogger<OrganizationDTO> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<bool> Add(ReferentViewModel obj)
        {
            string apiUrl = $"Add";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var objAdd = new ReferentDTO()
                {
                    ReferentId = obj.ReferentId,
                    ReferentRUT = obj.ReferentRUT,
                    ReferentDV = obj.ReferentDV,
                    ReferentFirstName = obj.ReferentFirstName,
                    ReferentLastName = obj.ReferentLastName,
                    ReferentCode = obj.ReferentCode,
                    ReferentEmail = obj.ReferentEmail,
                    ReferentPhone = obj.ReferentPhone,
                    ReferentBirthDay = obj.ReferentBirthDay,
                    OrganizationId = obj.OrganizationId,
                    OrganizationName = obj.OrganizationName,
                    CreationDate = obj.CreationDate,
                    IsActive = false,
                    IsDeleted = false
                };
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(objAdd), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.ToString());
                throw new Exception("Error en Integración - Intente mas tarde.");
            }
        }

        public async Task<bool> Delete(int idObj)
        {
            string apiUrl = $"Delete";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var obj = await Get(idObj);
            try
            {
                var objDelete = new ReferentDTO()
                {
                    ReferentId = obj.ReferentId,
                    ReferentRUT = obj.ReferentRUT,
                    ReferentFirstName = obj.ReferentFirstName,
                    ReferentLastName = obj.ReferentLastName,
                    ReferentEmail = obj.ReferentEmail,
                    ReferentPhone = obj.ReferentPhone,
                    ReferentBirthDay = obj.ReferentBirthDay,
                    OrganizationId = obj.OrganizationId,
                    IsActive = false,
                    IsDeleted = true
                };
                var content = new StringContent(JsonConvert.SerializeObject(objDelete), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);
                request.Content = content;
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.ToString());
                throw new Exception("Error en Integración - Intente mas tarde.");
            }
        }

        public async Task<bool> Edit(ReferentViewModel obj)
        {
            string apiUrl = $"Update";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var objEdit = new ReferentDTO()
                {
                    ReferentId = obj.ReferentId,
                    ReferentRUT = obj.ReferentRUT,
                    ReferentFirstName = obj.ReferentFirstName,
                    ReferentLastName = obj.ReferentLastName,
                    ReferentEmail = obj.ReferentEmail,
                    ReferentPhone = obj.ReferentPhone,
                    ReferentBirthDay = obj.ReferentBirthDay,
                    OrganizationId = obj.OrganizationId,
                    IsActive = obj.IsActive,
                    IsDeleted = false
                };
                HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(objEdit), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.ToString());
                throw new Exception("Error en Integración - Intente mas tarde.");
            }
        }

        public async Task<ReferentViewModel> Get(int id)
        {
            string apiUrl = $"" + id;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var dato = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ReferentViewModel>(dato);
                return responseData;
            }
            return null;
        }

        public async Task<ReferentViewModel> GetByRut(int referentRut)
        {
            string apiUrl = $"GetByRut/" + referentRut;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var dato = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ReferentViewModel>(dato);
                return responseData;
            }
            return null;
        }

        public async Task<IEnumerable<ReferentViewModel>> GetList()
        {
            string apiUrl = $"GetList";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<IEnumerable<ReferentViewModel>>(datos);
                    return responseData;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.ToString());
                throw new Exception("Error en Integración - Intente mas tarde.");
            }
        }
    }
}
