using SeminarHub.Data.DataModels;
using SeminarHub.Models;

namespace SeminarHub.Contracts
{
    public interface ISeminarService
    {
        Task AddSeminarAsync(AddSeminarViewModel model, string userId);

        Task<AddSeminarViewModel> GetAddSeminarViewModelAsync();

        Task<IEnumerable<AllSeminarViewModel>> GetAllSeminarsAsync();

        Task<DetailsSeminarViewModel> GetSeminarDetails(int id);

        Task<JoinSeminarViewModel?> GetSeminarByIdAsync(int id);

        Task AddSeminarToJoinedAsync(string userId, JoinSeminarViewModel seminarToJoin);

        Task<IEnumerable<JoinedSeminarViewModel>> GetJoinedSeminarsAsync(string userId);

        Task LeaveSeminarAsync(string userId, JoinSeminarViewModel seminarToLeave);

        Task<AddSeminarViewModel> GetSeminarToEdit(int id);

        Task<Seminar> FindSeminarAsync(int id);

        Task EditSeminarAsync(AddSeminarViewModel model, Seminar seminarToEdit);

        Task DeleteSeminarAsync(Seminar seminarToDel);
    }
}
