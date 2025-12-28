using Microsoft.AspNetCore.Mvc;
using intproje.Mvc.Models;
using intproje.Mvc.Services;

namespace intproje.Mvc.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly ApiService _apiService;

        public ApplicantsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Applicants
        public async Task<IActionResult> Index()
        {
            var Applicants = await _apiService.GetAsync<Applicant>("Applicants");
            return View(Applicants ?? new List<Applicant>());
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var applicant = await _apiService.GetByIdAsync<Applicant>("Applicants", id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }

        // GET: Applicants/Create
        public async Task<IActionResult> Create()
        {
            var Jobs = await _apiService.GetAsync<JobPost>("Jobs");
            ViewBag.Jobs = Jobs ?? new List<JobPost>();
            return View();
        }

        // POST: Applicants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiService.PostAsync<Applicant>("Applicants", applicant);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var Jobs = await _apiService.GetAsync<JobPost>("Jobs");
            ViewBag.Jobs = Jobs ?? new List<JobPost>();
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var applicant = await _apiService.GetByIdAsync<Applicant>("Applicants", id);
            if (applicant == null)
            {
                return NotFound();
            }
            var Jobs = await _apiService.GetAsync<JobPost>("Jobs");
            ViewBag.Jobs = Jobs ?? new List<JobPost>();
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _apiService.PutAsync<Applicant>("Applicants", id, applicant);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var Jobs = await _apiService.GetAsync<JobPost>("Jobs");
            ViewBag.Jobs = Jobs ?? new List<JobPost>();
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var applicant = await _apiService.GetByIdAsync<Applicant>("Applicants", id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _apiService.DeleteAsync("Applicants", id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}

