using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioMH.Models;

namespace PortfolioMH.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private const string SESSION_KEY = "AdminLoggedIn";

        public AdminController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private bool IsAuthenticated() =>
            HttpContext.Session.GetString(SESSION_KEY) == "true";

        // ─── AUTH ───────────────────────────────────────────────
        // GET: /Admin/Login
        public IActionResult Login()
        {
            if (IsAuthenticated()) return RedirectToAction("Index");
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string password)
        {
            var adminPassword = _config["AdminSettings:Password"];
            if (password == adminPassword)
            {
                HttpContext.Session.SetString(SESSION_KEY, "true");
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Incorrect password. Try again.";
            return View();
        }

        // GET: /Admin/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ─── DASHBOARD ──────────────────────────────────────────
        // GET: /Admin
        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            ViewBag.Messages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt).ToListAsync();

            ViewBag.Projects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt).ToListAsync();

            ViewBag.UnreadCount = await _context.ContactMessages
                .CountAsync(m => !m.IsRead);

            return View();
        }

        // ─── MESSAGES ───────────────────────────────────────────
        // POST: /Admin/MarkRead
        [HttpPost]
        public async Task<IActionResult> MarkRead(int id)
        {
            if (!IsAuthenticated()) return Json(new { success = false });
            var msg = await _context.ContactMessages.FindAsync(id);
            if (msg != null) { msg.IsRead = true; await _context.SaveChangesAsync(); }
            return Json(new { success = true });
        }

        // POST: /Admin/DeleteMessage
        [HttpPost]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (!IsAuthenticated()) return Json(new { success = false });
            var msg = await _context.ContactMessages.FindAsync(id);
            if (msg != null) { _context.ContactMessages.Remove(msg); await _context.SaveChangesAsync(); }
            return Json(new { success = true });
        }

        // ─── PROJECTS ───────────────────────────────────────────
        // POST: /Admin/AddProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProject(
            string title, string description, string technologies,
            string? liveUrl, string? gitHubUrl, string? imageUrl,
            bool isFeatured = false, bool isVisible = true)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(description))
            {
                _context.Projects.Add(new Project
                {
                    Title = title.Trim(),
                    Description = description.Trim(),
                    Technologies = technologies?.Trim() ?? "",
                    LiveUrl = liveUrl?.Trim(),
                    GitHubUrl = gitHubUrl?.Trim(),
                    ImageUrl = imageUrl?.Trim(),
                    IsFeatured = isFeatured,
                    IsVisible = isVisible,
                    CreatedAt = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
                TempData["Success"] = "Project added successfully!";
            }
            return RedirectToAction("Index");
        }

        // POST: /Admin/ToggleProjectVisibility
        [HttpPost]
        public async Task<IActionResult> ToggleVisibility(int id)
        {
            if (!IsAuthenticated()) return Json(new { success = false });
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                project.IsVisible = !project.IsVisible;
                await _context.SaveChangesAsync();
            }
            return Json(new { success = true, isVisible = project?.IsVisible });
        }

        // POST: /Admin/DeleteProject
        [HttpPost]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (!IsAuthenticated()) return Json(new { success = false });
            var project = await _context.Projects.FindAsync(id);
            if (project != null) { _context.Projects.Remove(project); await _context.SaveChangesAsync(); }
            return Json(new { success = true });
        }
    }
}
