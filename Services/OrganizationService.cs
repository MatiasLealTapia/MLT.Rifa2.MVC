using MLT.Rifa2.MVC.DTOs;
using MLT.Rifa2.MVC.Generic;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MLT.Rifa2.MVC.Services
{
    public class OrganizationService : IOrganizationService
    {
        private HttpClient _httpClient;
        private readonly ILogger<OrganizationDTO> _logger;

        public OrganizationService(HttpClient httpClient, ILogger<OrganizationDTO> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> Add(OrganizationViewModel obj)
        {
            string apiUrl = $"Add";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var objAdd = new OrganizationDTO()
                {
                    OrganizationId = obj.OrganizationId,
                    OrganizationName = obj.OrganizationName,
                    OrganizationTypeId = obj.OrganizationTypeId,
                    OrganizationTypeName = obj.OrganizationTypeName,
                    CreationDate = DateTime.Now,
                    IsActive = obj.IsActive,
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
                var objDelete = new OrganizationDTO()
                {
                    OrganizationId = obj.OrganizationId,
                    OrganizationName = obj.OrganizationName,
                    OrganizationTypeId = obj.OrganizationTypeId,
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

        public async Task<bool> Edit(OrganizationViewModel obj)
        {
            string apiUrl = $"Update";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var objEdit = new OrganizationDTO()
                {
                    OrganizationId = obj.OrganizationId,
                    OrganizationName = obj.OrganizationName,
                    OrganizationTypeId = obj.OrganizationTypeId,
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

        public async Task<OrganizationViewModel> Get(int id)
        {
            string apiUrl = $"" + id;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var dato = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<OrganizationViewModel>(dato);
                return responseData;
            }
            return null;
        }

        public async Task<IEnumerable<OrganizationViewModel>> GetList()
        {
            string apiUrl = $"GetList";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<IEnumerable<OrganizationViewModel>>(datos);
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

        public async Task<OrganizationViewModel> Login(OrganizationLogIn orgLogin)
        {
            string apiUrl = $"Login";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var passwordHash = Tools.Encrypt(orgLogin.Password);
                var orgLoginDTO = new OrgAdminLogInDTO()
                {
                    OrgAdminEmail = orgLogin.Email,
                    OrgAdminPasswordHash = passwordHash
                };
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(orgLoginDTO), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<OrganizationDTO>(data);
                    var orgViewModel = new OrganizationViewModel
                    {
                        OrganizationId = responseData.OrganizationId,
                        OrganizationName = responseData.OrganizationName,
                        OrganizationTypeId = responseData.OrganizationTypeId,
                        OrganizationTypeName = responseData.OrganizationTypeName,
                        CreationDate = responseData.CreationDate
                    };
                    return orgViewModel;
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
