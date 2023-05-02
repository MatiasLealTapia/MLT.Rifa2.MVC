using MLT.Rifa2.MVC.ViewModel;

namespace MLT.Rifa2.MVC.Interfaces
{
    public interface IOrganizationService
    {
        Task<bool> Add(OrganizationViewModel obj);
        Task<bool> Delete(int idObj);
        Task<bool> Edit(OrganizationViewModel obj);
        Task<OrganizationViewModel> Get(int id);
        Task<IEnumerable<OrganizationViewModel>> GetList();
    }
}
