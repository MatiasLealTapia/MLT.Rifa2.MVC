using MLT.Rifa2.MVC.DTOs;

namespace MLT.Rifa2.MVC.Interfaces
{
    public interface IGenericService
    {
        Task<List<GenericItemDTO>> GetOrganizationTypes();
        Task<GenericItemDTO> GetOrganizationType(int idObj);
        Task<List<GenericItemDTO>> GetOrganizationsByType(int idType);
        Task<List<GenericItemDTO>> GetOrganizations();
        Task<GenericItemDTO> GetOrganization(int idObj);
        Task<List<GenericItemDTO>> GetReferents();
        Task<GenericItemDTO> GetReferent(int idObj);
        Task<GenericItemDTO> GetReferentByRut(int referentRut);
        Task<List<GenericItemDTO>> GetReferentsByOrganizationId(int organizationId);
        Task<GenericItemDTO> GetReferentByEmail(string referentEmail);
    }
}
