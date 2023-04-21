using MLT.Rifa2.MVC.DTOs;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;

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
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int idObj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(OrganizationTypeViewModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationTypeViewModel> Get(int id)
        {
            throw new NotImplementedException();
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
                    var responseData = JsonConvert.DeserializeObject<IEnumerable<SexViewModel>>(datos);
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
