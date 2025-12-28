using Microsoft.AspNetCore.Mvc;
using intproje.Mvc.Models;
using intproje.Mvc.Services;

namespace intproje.Mvc.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApiService _apiService;

        public CompaniesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var companies = await _apiService.GetAsync<Company>("Companies");
            return View(companies ?? new List<Company>());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var company = await _apiService.GetByIdAsync<Company>("Companies", id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiService.PostAsync<Company>("Companies", company);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _apiService.GetByIdAsync<Company>("Companies", id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _apiService.PutAsync<Company>("Companies", id, company);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _apiService.GetByIdAsync<Company>("Companies", id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _apiService.DeleteAsync("Companies", id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}

