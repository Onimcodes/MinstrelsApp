using Microsoft.AspNetCore.Mvc;
using Minstrels.Interfaces;
using Minstrels.Models;
using Minstrels.ViewModels;

namespace Minstrels.Controllers
{
    public class RehearsalController : Controller
    {
        private readonly IRehearsalRepository _rehearsalRepository;
        private readonly IPhotoService photoService;

        public RehearsalController(IRehearsalRepository rehearsalRepository,IPhotoService photoService)
        {
           _rehearsalRepository = rehearsalRepository;
            this.photoService = photoService;
        }
        public async  Task<IActionResult> Index()
        {
            var rehearsals =  await _rehearsalRepository.GetAll(); 

            return View(rehearsals);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var rehearsal = await _rehearsalRepository.GetByIdAsync(id);

            return View(rehearsal);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRehearsalViewModel rehearsalVm)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(rehearsalVm.Image);
                var rehearsal = new Rehearsal
                {
                    Description = rehearsalVm.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = rehearsalVm.Address.Street,
                        City = rehearsalVm.Address.City,
                        State = rehearsalVm.Address.State
                    },
                };
                _rehearsalRepository.Add(rehearsal);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(rehearsalVm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rehearsal = await _rehearsalRepository.GetByIdAsync(id);
            if (rehearsal == null) return View("Error");
            var rehearsalVm = new EditRehearsalViewModel
            {
                Description = rehearsal.Description,
                Address = rehearsal.Address,    
                URL = rehearsal.Image
            };
            return View(rehearsalVm);
        }

        [HttpPost]

        public async Task<IActionResult> Edit (int id, EditRehearsalViewModel rehearsalVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", rehearsalVm);
            }
            var userRehearsal = await _rehearsalRepository.GetByIdAsyncNoTracking(id);
            if(userRehearsal != null)
            {
                try
                {
                    await photoService.DeletePhotoAsync(userRehearsal.Image);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Could not delete photo");
                    return View(rehearsalVm);
                }
                var photoResult = await photoService.AddPhotoAsync(rehearsalVm.Image);
                var rehearsal = new Rehearsal
                {
                    Id = id,
                    Description = rehearsalVm.Description,
                    Image = photoResult.Url.ToString(),
                    Address = rehearsalVm.Address
                };
                _rehearsalRepository.Update(rehearsal);
                return RedirectToAction("index", "rehearsal");
            }
            else
            {
                return View(rehearsalVm);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var rehearsaldetails = await _rehearsalRepository.GetByIdAsync(id);
            if (rehearsaldetails == null) return View("Error");
            return View(rehearsaldetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRehearsal(int id)
        {
            var rehearsaldetails = await _rehearsalRepository.GetByIdAsync(id);
            if (rehearsaldetails == null) return View("Error");
            _rehearsalRepository.Delete(rehearsaldetails);
            return RedirectToAction("index");
        }
    }
}
