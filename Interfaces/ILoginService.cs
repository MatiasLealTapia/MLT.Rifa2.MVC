using MLT.Rifa2.MVC.ViewModel;

namespace MLT.Rifa2.MVC.Interfaces
{
    public interface ILoginService
    {
        //public LogInUserViewModel LoginUser(LogInUserViewModel model);
        public Task<bool> PostOrganizationForm(OrganizationFormViewModel model);
    }
}
