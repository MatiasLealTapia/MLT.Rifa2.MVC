using MLT.Rifa2.MVC.ViewModel;

namespace MLT.Rifa2.MVC.Interfaces
{
    public interface IRaffleService
    {
        Task<bool> CreateRaffle(RaffleViewModel raffle);
        Task<IEnumerable<RaffleViewModel>> GetListByOrganization(int orgId);
        Task<RaffleViewModel> GetRaffleById(int raffleId);
    }
}
