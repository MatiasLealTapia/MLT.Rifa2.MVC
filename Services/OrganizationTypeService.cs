using MLT.Rifa2.MVC.DTOs;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MLT.Rifa2.MVC.Services
{
    public class OrganizationTypeService : IOrganizationTypeService
    {
        private HttpClient _httpClient;
        private readonly ILogger<OrganizationTypeDTO> _logger;

        public OrganizationTypeService(HttpClient httpClient, ILogger<OrganizationTypeDTO> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<bool> Add(OrganizationTypeViewModel obj)
        {
            string apiUrl = $"Add";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var objAdd = new OrganizationTypeDTO()
                {
                    OrganizationTypeId = obj.OrganizationTypeId,
                    OrganizationTypeName = obj.OrganizationTypeName,
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
                var objDelete = new OrganizationTypeDTO()
                {
                    OrganizationTypeId = obj.OrganizationTypeId,
                    OrganizationTypeName = obj.OrganizationTypeName,
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
        public async Task<bool> Edit(OrganizationTypeViewModel obj)
        {
            string apiUrl = $"Update";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var objEdit = new OrganizationTypeDTO()
                {
                    OrganizationTypeId = obj.OrganizationTypeId,
                    OrganizationTypeName = obj.OrganizationTypeName,
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
        public async Task<OrganizationTypeViewModel> Get(int id)
        {
            string apiUrl = $"" + id;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var dato = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<OrganizationTypeViewModel>(dato);
                return responseData;
            }
            return null;
        }
        public async Task<IEnumerable<OrganizationTypeViewModel>> GetList()
        {
            string apiUrl = $"GetList";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<IEnumerable<OrganizationTypeViewModel>>(datos);
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
