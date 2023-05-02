using MLT.Rifa2.MVC.DTOs;
using MLT.Rifa2.MVC.Interfaces;
using Newtonsoft.Json;

namespace MLT.Rifa2.MVC.Services
{
    public class GenericService : IGenericService
    {
        private HttpClient _httpClient;
        private readonly ILogger<GenericItemDTO> _logger;

        public GenericService(HttpClient httpClient, ILogger<GenericItemDTO> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<List<GenericItemDTO>> GetOrganizationsByType(int idType)
        {
            string apiUrl = $"GetOrganizationsByType";
            return null;
        }

        public async Task<GenericItemDTO> GetOrganizationType(int idObj)
        {
            string apiUrl = $"GetOrganizationType";
            return null;
        }

        public async Task<List<GenericItemDTO>> GetOrganizationTypes()
        {
            string apiUrl = $"GetOrganizationTypes";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<List<GenericItemDTO>>(datos);
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
