using Microsoft.AspNetCore.Mvc;
using intproje.Mvc.Models;
using intproje.Mvc.Services;

namespace intproje.Mvc.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApiService _apiService;

        public JobsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var Jobs = await _apiService.GetAsync<JobPost>("Jobs");
            return View(Jobs ?? new List<JobPost>());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var job = await _apiService.GetByIdAsync<JobPost>("Jobs", id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public async Task<IActionResult> Create()
        {
            var Companies = await _apiService.GetAsync<Company>("Companies");
            ViewBag.Companies = Companies ?? new List<Company>();
            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiService.PostAsync<JobPost>("Jobs", jobPost);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var Companies = await _apiService.GetAsync<Company>("Companies");
            ViewBag.Companies = Companies ?? new List<Company>();
            return View(jobPost);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var job = await _apiService.GetByIdAsync<JobPost>("Jobs", id);
            if (job == null)
            {
                return NotFound();
            }
            var Companies = await _apiService.GetAsync<Company>("Companies");
            ViewBag.Companies = Companies ?? new List<Company>();
            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobPost jobPost)
        {
            if (id != jobPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _apiService.PutAsync<JobPost>("Jobs", id, jobPost);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var Companies = await _apiService.GetAsync<Company>("Companies");
            ViewBag.Companies = Companies ?? new List<Company>();
            return View(jobPost);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _apiService.GetByIdAsync<JobPost>("Jobs", id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _apiService.DeleteAsync("Jobs", id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: Jobs/Apply/5
        public async Task<IActionResult> Apply(int id)
        {
            var job = await _apiService.GetByIdAsync<JobPost>("Jobs", id);
            if (job == null)
            {
                return NotFound();
            }
            ViewBag.JobId = id;
            ViewBag.JobTitle = job.Title;
            return View(new Applicant { JobPostId = id });
        }

        // POST: Jobs/Apply/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiService.PostAsync<Applicant>("Applicants", applicant);
                if (result != null)
                {
                    return RedirectToAction(nameof(Details), new { id = applicant.JobPostId });
                }
            }
            var job = await _apiService.GetByIdAsync<JobPost>("Jobs", applicant.JobPostId);
            if (job != null)
            {
                ViewBag.JobTitle = job.Title;
            }
            return View(applicant);
        }
    }
}

