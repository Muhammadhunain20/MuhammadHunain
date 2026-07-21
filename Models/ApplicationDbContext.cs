using Microsoft.EntityFrameworkCore;

namespace PortfolioMH.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }

    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Projects.Any()) return;

            context.Projects.AddRange(new List<Project>
            {
                new Project
                {
                    Title = "ShopEase – E-Commerce Platform",
                    Description = "A full-stack e-commerce web application built with ASP.NET MVC 8 and SQL Server. Features include product catalog with category filtering, user registration & authentication, shopping cart, order management, and an admin dashboard. Deployed live on Microsoft Azure (UAE North region).",
                    Technologies = "ASP.NET MVC 8,SQL Server,HTML5,CSS3,Bootstrap 5,JavaScript,Azure",
                    LiveUrl = "https://shopeasev2-hah9gga2ekc9hxc4.uaenorth-01.azurewebsites.net/",
                    GitHubUrl = "https://github.com/Muhammadhunain20/ShopEase",
                    IsVisible = true,
                    IsFeatured = true,
                    CreatedAt = new DateTime(2024, 10, 1)
                },
                new Project
                {
                    Title = "PrimeInspire – Online Courses Platform",
                    Description = "A fully responsive multi-page e-learning website featuring a home page, course listings with pricing cards, blog section, FAQ accordion, About page, and contact form. Built with Bootstrap 5 grid system and custom CSS — deployed on GitHub Pages.",
                    Technologies = "HTML5,CSS3,Bootstrap 5,JavaScript,GitHub Pages",
                    LiveUrl = "https://muhammadhunain20.github.io/Online_Courses",
                    GitHubUrl = "https://github.com/muhammadhunain20",
                    IsVisible = true,
                    IsFeatured = false,
                    CreatedAt = new DateTime(2024, 6, 1)
                },
                new Project
                {
                    Title = "Employee Management System",
                    Description = "A web-based HR management UI with secure login, role-based navigation, and a clean admin dashboard. Features form validation, structured component design, and a professional Bootstrap layout that mirrors real-world server-side authentication patterns.",
                    Technologies = "HTML5,CSS3,Bootstrap 5,JavaScript,GitHub Pages",
                    LiveUrl = "https://muhammadhunain20.github.io/Employee_Managment_System",
                    GitHubUrl = "https://github.com/muhammadhunain20",
                    IsVisible = true,
                    IsFeatured = false,
                    CreatedAt = new DateTime(2024, 4, 1)
                },
                new Project
                {
                    Title = "Responsive Blog Website",
                    Description = "A multi-page blog platform (Home, Categories, Posts, Search) with post card components, newsletter subscription section, and a fully responsive Bootstrap navbar. Structured for scalable content publishing, similar to a WordPress content model.",
                    Technologies = "HTML5,CSS3,Bootstrap 5,GitHub Pages",
                    LiveUrl = "https://muhammadhunain20.github.io/Blog",
                    GitHubUrl = "https://github.com/muhammadhunain20",
                    IsVisible = true,
                    IsFeatured = false,
                    CreatedAt = new DateTime(2024, 2, 1)
                }
            });

            context.SaveChanges();
        }
    }
}
