using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SeminarHub.Contracts;
using SeminarHub.Data;
using SeminarHub.Data.DataModels;
using SeminarHub.Models;
using System.Globalization;
using System.Linq.Expressions;
using static SeminarHub.Data.DataConstants.DataConstants;

namespace SeminarHub.Services
{
    public class SeminarService : ISeminarService
    {
        private readonly SeminarHubDbContext context;

        public SeminarService(SeminarHubDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task AddSeminarAsync(AddSeminarViewModel model, string userId)
        {
            string dateTimeString = $"{model.DateAndTime}";

            if (!DateTime.TryParseExact(dateTimeString, SeminarDateAndTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                throw new InvalidOperationException("Invalid date or time format");
            }

            var seminarToAdd = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = userId,
                DateAndTime = parsedDateTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId
            };

            await context.Seminars.AddAsync(seminarToAdd);

            await context.SaveChangesAsync();
        }

        public async Task AddSeminarToJoinedAsync(string userId, JoinSeminarViewModel seminarToJoin)
        {
            bool isAlreadyAdded = await context.SeminarsParticipants
                .AnyAsync(sp => sp.ParticipantId == userId && sp.SeminarId == seminarToJoin.Id);

            if (!isAlreadyAdded)
            {
                var seminarParticipant = new SeminarParticipant()
                {
                    ParticipantId = userId,
                    SeminarId = seminarToJoin.Id
                };

                await context.SeminarsParticipants.AddAsync(seminarParticipant);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteSeminarAsync(Seminar seminarToDel)
        {
            context.Seminars.Remove(seminarToDel);

            await context.SaveChangesAsync();
        }

        public async Task EditSeminarAsync(AddSeminarViewModel model, Seminar seminarToEdit)
        {
            string dateTimeString = $"{model.DateAndTime}";

            if (!DateTime.TryParseExact(dateTimeString, SeminarDateAndTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                throw new InvalidOperationException("Invalid date or time format");
            }

            seminarToEdit.Topic = model.Topic;
            seminarToEdit.Lecturer = model.Lecturer;
            seminarToEdit.Details = model.Details;
            seminarToEdit.DateAndTime = parsedDateTime;
            seminarToEdit.Duration = model.Duration;
            seminarToEdit.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();
        }

        public async Task<Seminar> FindSeminarAsync(int id)
        {
            Seminar seminar = await context.Seminars.FindAsync(id);

            return seminar;
        }

        public async Task<AddSeminarViewModel> GetAddSeminarViewModelAsync()
        {
            var categories = await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            var model = new AddSeminarViewModel()
            {
                Categories = categories
            };

            return model;
        }

        public async Task<IEnumerable<AllSeminarViewModel>> GetAllSeminarsAsync()
        {
            return await context.Seminars
                .AsNoTracking()
                .Select(s => new AllSeminarViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Organizer = s.Organizer.UserName,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(SeminarDateAndTimeFormat)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<JoinedSeminarViewModel>> GetJoinedSeminarsAsync(string userId)
        {
            return await context.SeminarsParticipants
                .AsNoTracking()
                .Where(sp => sp.ParticipantId == userId)
                .Select(sp => new JoinedSeminarViewModel()
                {
                    Id = sp.Seminar.Id,
                    Topic = sp.Seminar.Topic,
                    Lecturer = sp.Seminar.Lecturer,
                    DateAndTime = sp.Seminar.DateAndTime.ToString(SeminarDateAndTimeFormat),
                    Category = sp.Seminar.Category.Name,
                    Organizer = sp.Seminar.Organizer.UserName
                })
                .ToListAsync();
        }

        public async Task<JoinSeminarViewModel?> GetSeminarByIdAsync(int id)
        {
            var seminar = await context.Seminars
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new JoinSeminarViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Details = s.Details,
                    OrganizerId = s.OrganizerId,
                    DateAndTime = s.DateAndTime,
                    Duration = s.Duration != null ? s.Duration.Value : 0,
                    CategoryId = s.CategoryId
                })
                .FirstOrDefaultAsync();

            return seminar;
        }

        public async Task<DetailsSeminarViewModel> GetSeminarDetails(int id)
        {
            var seminarInDetails = await context.Seminars
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new DetailsSeminarViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    OrganizerId = s.OrganizerId,
                    Organizer = s.Organizer.UserName,
                    Details = s.Details,
                    Duration = s.Duration != null ? s.Duration.Value : 0,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(SeminarDateAndTimeFormat)
                })
                .FirstOrDefaultAsync();

            if (seminarInDetails == null)
            {
                throw new InvalidOperationException("Invalid seminar!");
            }

            return seminarInDetails;
        }

        public async Task<AddSeminarViewModel> GetSeminarToEdit(int id)
        {
            var categories = await context.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            var seminarToEdit = await context.Seminars
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new AddSeminarViewModel()
                {
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Details = s.Details,
                    DateAndTime = s.DateAndTime.ToString(SeminarDateAndTimeFormat),
                    Duration = s.Duration != null ? s.Duration.Value : 0,
                    OrganizerId = s.OrganizerId,
                    CategoryId = s.CategoryId,
                    Categories = categories
                })
                .FirstOrDefaultAsync();

            return seminarToEdit;
        }

        public async Task LeaveSeminarAsync(string userId, JoinSeminarViewModel seminarToLeave)
        {
            var seminarParticipantToLeave = await context.SeminarsParticipants
                .FirstOrDefaultAsync(sp => sp.SeminarId == seminarToLeave.Id && sp.ParticipantId == userId);

            context.SeminarsParticipants.Remove(seminarParticipantToLeave);
            await context.SaveChangesAsync();
        }
    }
}
