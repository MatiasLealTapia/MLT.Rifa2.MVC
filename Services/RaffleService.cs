using MLT.Rifa2.MVC.DTOs;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MLT.Rifa2.MVC.Services
{
    public class RaffleService : IRaffleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RaffleDTO> _logger;

        public RaffleService(HttpClient httpClient, ILogger<RaffleDTO> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> CreateRaffle(RaffleViewModel raffle)
        {
            string apiUrl = $"CreateRaffle";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var newRaffle = new RaffleDTO()
                {
                    RaffleName = raffle.RaffleName,
                    RaffleDescription = raffle.RaffleDescription,
                    RaffleNumberPrice = raffle.RaffleNumberPrice,
                    RaffleNumbersAmount = raffle.RaffleNumbersAmount,
                    RaffleBeginDate = raffle.RaffleBeginDate,
                    RaffleEndDate = raffle.RaffleEndDate,
                    OrganizationId = raffle.OrganizationId,
                    OrganizationName = raffle.OrganizationName,
                    RaffleCreationDate = raffle.RaffleCreationDate,
                    IsActive = false,
                    IsDeleted = false
                };
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(newRaffle), Encoding.UTF8, "application/json"));
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

        public async Task<IEnumerable<RaffleViewModel>> GetListByOrganization(int orgId)
        {
            string apiUrl = $"GetListByOrganization/" + orgId;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var datos = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<IEnumerable<RaffleViewModel>>(datos);
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

        public async Task<RaffleViewModel> GetRaffleById(int raffleId)
        {
            string apiUrl = $"GetRaffleById" + raffleId;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var dato = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<RaffleDTO>(dato);
                var raffleVM = new RaffleViewModel
                {
                    RaffleId = responseData.RaffleId,
                    RaffleName = responseData.RaffleName,
                    RaffleDescription = responseData.RaffleDescription,
                    RaffleNumberPrice = responseData.RaffleNumberPrice,
                    RaffleNumbersAmount = responseData.RaffleNumbersAmount,
                    RaffleBeginDate = responseData.RaffleBeginDate,
                    RaffleEndDate = responseData.RaffleEndDate,
                    OrganizationId = responseData.OrganizationId,
                    OrganizationName = responseData.OrganizationName,
                    RaffleCreationDate = responseData.RaffleCreationDate,
                    IsActive = responseData.IsActive
                };
                return raffleVM;
            }
            return null;
        }
    }
}
