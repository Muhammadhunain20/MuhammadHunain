using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioMH.Models;

namespace PortfolioMH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /  or /Home/Index
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Where(p => p.IsVisible)
                .OrderByDescending(p => p.IsFeatured)
                .ThenByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(projects);
        }

        // POST: /Home/SubmitContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitContact(
            string name, string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(message))
            {
                return Json(new { success = false, message = "Please fill in all fields." });
            }

            var contact = new ContactMessage
            {
                Name = name.Trim(),
                Email = email.Trim(),
                Subject = subject.Trim(),
                Message = message.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.ContactMessages.Add(contact);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Thanks! I'll get back to you soon." });
        }

        // GET: /Home/DownloadCV
        public IActionResult DownloadCV()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "Muhammad_Hunain_CV.pdf");
            if (!System.IO.File.Exists(filePath))
                return NotFound("CV file not found. Please add your CV to wwwroot/files/Muhammad_Hunain_CV.pdf");

            return PhysicalFile(filePath, "application/pdf", "Muhammad_Hunain_CV.pdf");
        }
    }
}
