using MLT.Rifa2.MVC.DTOs;

namespace MLT.Rifa2.MVC.Interfaces
{
    public interface IGenericService
    {
        Task<List<GenericItemDTO>> GetOrganizationTypes();
        Task<GenericItemDTO> GetOrganizationType(int idObj);
        Task<List<GenericItemDTO>> GetOrganizationsByType(int idType);
    }
}
