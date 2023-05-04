using MLT.Rifa2.MVC.ViewModel;

namespace MLT.Rifa2.MVC.Interfaces
{
    public interface IReferentService
    {
        Task<bool> Add(ReferentViewModel obj);
        Task<bool> Delete(int idObj);
        Task<bool> Edit(ReferentViewModel obj);
        Task<ReferentViewModel> Get(int id);
        Task<ReferentViewModel> GetByRut(int referentRut);
        Task<IEnumerable<ReferentViewModel>> GetList();
    }
}
