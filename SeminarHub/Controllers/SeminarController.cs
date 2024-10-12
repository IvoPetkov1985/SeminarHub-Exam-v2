using Microsoft.AspNetCore.Mvc;
using SeminarHub.Contracts;
using SeminarHub.Models;

namespace SeminarHub.Controllers
{
    public class SeminarController : BaseController
    {
        private readonly ISeminarService service;

        public SeminarController(ISeminarService seminarService)
        {
            service = seminarService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddSeminarViewModel model = await service.GetAddSeminarViewModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSeminarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = GetUserId();

            await service.AddSeminarAsync(model, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await service.GetAllSeminarsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await service.GetSeminarDetails(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        public async Task<IActionResult> Join(int id)
        {
            var seminarToJoin = await service.GetSeminarByIdAsync(id);

            if (seminarToJoin == null)
            {
                return RedirectToAction(nameof(All));
            }

            string userId = GetUserId();

            await service.AddSeminarToJoinedAsync(userId, seminarToJoin);

            return RedirectToAction(nameof(Joined));
        }

        public async Task<IActionResult> Joined()
        {
            var model = await service.GetJoinedSeminarsAsync(GetUserId());

            return View(model);
        }

        public async Task<IActionResult> Leave(int id)
        {
            var seminarToLeave = await service.GetSeminarByIdAsync(id);

            if (seminarToLeave == null)
            {
                return RedirectToAction(nameof(Joined));
            }

            string userId = GetUserId();

            await service.LeaveSeminarAsync(userId, seminarToLeave);

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddSeminarViewModel seminarToEdit = await service.GetSeminarToEdit(id);

            if (seminarToEdit == null)
            {
                return RedirectToAction(nameof(All));
            }

            string userId = GetUserId();

            if (userId != seminarToEdit.OrganizerId)
            {
                return RedirectToAction(nameof(All));
            }

            return View(seminarToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddSeminarViewModel model)
        {
            var seminarToedit = await service.FindSeminarAsync(id);

            if (seminarToedit == null || ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(All));
            }

            string currentUser = GetUserId();

            if (currentUser != seminarToedit.OrganizerId)
            {
                return RedirectToAction(nameof(All));
            }

            await service.EditSeminarAsync(model, seminarToedit);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seminarToDelete = await service.FindSeminarAsync(id);

            string userId = GetUserId();

            if (seminarToDelete == null || userId != seminarToDelete.OrganizerId)
            {
                return RedirectToAction(nameof(All));
            }

            DeleteSeminarViewModel model = new DeleteSeminarViewModel()
            {
                Id = seminarToDelete.Id,
                Topic = seminarToDelete.Topic,
                DateAndTime = seminarToDelete.DateAndTime
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminarToDel = await service.FindSeminarAsync(id);

            string userId = GetUserId();

            if (seminarToDel == null || userId != seminarToDel.OrganizerId)
            {
                return RedirectToAction(nameof(All));
            }

            await service.DeleteSeminarAsync(seminarToDel);

            return RedirectToAction(nameof(All));
        }
    }
}
