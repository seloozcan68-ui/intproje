using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using intproje.Mvc.Models;
using intproje.Mvc.Services; 

namespace intproje.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ApiService _apiService;

    public HomeController(ApiService apiService) 
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index() 
    {
        // API'den ilanları çekiyoruz
        var jobs = await _apiService.GetJobPostsAsync(); 
        
        // Önemli: Eğer API'den veri gelmezse (null ise) hata almamak için boş bir liste oluşturuyoruz
        var jobList = jobs ?? new List<JobPost>();
        
        // Son eklenen 4 ilanı alıp View'a gönderiyoruz
        var featuredJobs = jobList.OrderByDescending(x => x.Id).Take(4).ToList();
        
        return View(featuredJobs);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}