using MLT.Rifa2.MVC.ViewModel;

namespace MLT.Rifa2.MVC.Interfaces
{
    public interface IOrganizationTypeService
    {
        Task<bool> Add(OrganizationTypeViewModel obj);
        Task<bool> Delete(int idObj);
        Task<bool> Edit(OrganizationTypeViewModel obj);
        Task<OrganizationTypeViewModel> Get(int id);
        Task<IEnumerable<OrganizationTypeViewModel>> GetList();
    }
}
