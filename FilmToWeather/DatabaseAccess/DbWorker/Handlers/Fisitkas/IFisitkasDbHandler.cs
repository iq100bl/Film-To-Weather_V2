using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;

namespace DatabaseAccess.DbWorker.Handlers.Fisitkas
{
    public interface IFisitkasDbHandler : IGenericDbHandler<MainFisitkaForProjectModel>
    {
        Task<Dictionary<string, string>> GetAll();
        Task Update(Dictionary<string, string> updatedFisitkas);
    }
}
