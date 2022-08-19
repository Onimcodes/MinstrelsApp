using Microsoft.AspNetCore.Mvc;
using Minstrels.Interfaces;
using Minstrels.Models;
using Minstrels.ViewModels;

namespace Minstrels.Controllers
{
    public class ShowController : Controller
    {
        private readonly IShowRepository _showRepository;
        private readonly IPhotoService photoService;

        public ShowController(IShowRepository showRepository,IPhotoService photoService)
        {
            _showRepository = showRepository;
            this.photoService = photoService;
        }
        public async  Task<IActionResult> Index()
        {

            var Shows = await _showRepository.GetAll(); 
            return View(Shows);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);  
            return View(show);  
        }

        public  IActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShowViewModel showVm)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(showVm.Image);
                var show = new Show
                {
                    Theme = showVm.Theme,
                    Description = showVm.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = showVm.Address.Street,
                        City = showVm.Address.City,
                        State = showVm.Address.State
                    },
                };
                _showRepository.Add(show);  
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(showVm);  
        }


        public async Task<IActionResult> Edit(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);
            if (show == null) return View("Error");
            var showVM = new EditShowViewModel
            {
                Theme = show.Theme,
                Description = show.Description,
                Address = show.Address,
                URL = show.Image
            };
            return View(showVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id, EditShowViewModel showVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", showVM);
            }
            var userShow = await _showRepository.GetByIdAsyncNoTracking(id);
            if (userShow != null)
            {
                try
                {
                    await photoService.DeletePhotoAsync(userShow.Image);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Could not delete photo");
                    return View(showVM);
                }
                var photoResult = await photoService.AddPhotoAsync(showVM.Image);
                var show = new Show
                {

                    Id = id,
                    Theme = showVM.Theme,   
                    Description = showVM.Description,
                    Image = photoResult.Url.ToString(),
                    Address = showVM.Address
                };
                _showRepository.Update(show);
                return RedirectToAction("index", "show");
            }
            else
            {
                return View(showVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var showdetails = await _showRepository.GetByIdAsync(id);
            if (showdetails == null) return View("Error");
          return View(showdetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            var showdetails = await _showRepository.GetByIdAsync(id); 
            if(showdetails == null) return View("Error");
            _showRepository.Delete(showdetails);
            return RedirectToAction("index");
        }
    }
}
