# Muhammad Hunain – Portfolio Website

A full-stack personal portfolio built with **ASP.NET MVC 8**, **SQL Server**, and a custom dark-themed frontend (HTML/CSS/JS/Bootstrap 5).

---

## Tech Stack

| Layer      | Technology                                 |
|------------|--------------------------------------------|
| Backend    | ASP.NET MVC 8 (.NET 8)                     |
| Database   | SQL Server (Entity Framework Core 8)        |
| Frontend   | HTML5, CSS3, JavaScript (ES6+), Bootstrap 5 |
| Animations | AOS.js, Typed.js, VanillaTilt, Canvas API   |
| Fonts      | Space Grotesk, Inter (Google Fonts)         |
| Icons      | Font Awesome 6                              |
| Hosting    | Azure / any .NET-compatible host            |

---

## Features

- **Single-page portfolio** with Home, About, Projects, Contact sections
- **Particle network** canvas background on hero
- **Typing animation** for roles (Web Developer, ASP.NET Developer, etc.)
- **Animated skill bars** triggered on scroll
- **3D tilt effect** on project cards (VanillaTilt)
- **Contact form** with AJAX submission (no page reload)
- **Admin panel** at `/Admin` – manage messages and projects
- **Auto database seeding** with your 4 existing projects
- **Responsive** – works on mobile, tablet, desktop

---

## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- SQL Server (Express is fine) or SQL Server LocalDB
- Visual Studio 2022 or VS Code with C# extension

---

### Step 1 – Clone / Extract
```bash
cd PortfolioMH
```

### Step 2 – Update Connection String
Open `appsettings.json` and update:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=PortfolioMH;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
If you're using **LocalDB**, replace `Server=.` with:
```
Server=(localdb)\\mssqllocaldb
```

### Step 3 – Change Admin Password
In `appsettings.json`:
```json
"AdminSettings": {
  "Password": "YourNewSecurePassword123"
}
```

### Step 4 – Add Your Profile Photo
Copy your profile photo to:
```
wwwroot/images/profile.jpg
```
The hero section will display it automatically.

### Step 5 – Add Your CV
Copy your CV PDF to:
```
wwwroot/files/Muhammad_Hunain_CV.pdf
```
The "Download CV" button will serve this file.

### Step 6 – Add Project Screenshots (Optional)
To show screenshots instead of gradient placeholders:
1. Log in to Admin panel at `/Admin/Login`
2. Edit projects and add an Image URL field
   (You can use a hosted image URL, e.g. from Cloudinary or GitHub raw)

### Step 7 – Restore & Run
```bash
dotnet restore
dotnet run
```

The database will be **auto-created** on first run with your 4 projects already seeded.

Visit: `https://localhost:5001`

---

## Admin Panel

| URL             | Description          |
|-----------------|----------------------|
| `/Admin/Login`  | Login page           |
| `/Admin`        | Dashboard (messages + projects) |
| `/Admin/Logout` | Logout               |

Default password: `PortfolioAdmin@2024` ← **Change this!**

### Admin Features
- View all contact messages (unread highlighted)
- Mark messages as read
- Delete messages
- Add new projects (title, description, tech stack, live URL, GitHub URL)
- Toggle project visibility (show/hide on portfolio)
- Delete projects

---

## Project Structure

```
PortfolioMH/
├── Controllers/
│   ├── HomeController.cs       # Main portfolio + contact form
│   └── AdminController.cs      # Admin auth + CRUD
├── Models/
│   ├── Project.cs
│   ├── ContactMessage.cs
│   └── ApplicationDbContext.cs # EF Core context + DB seeder
├── Views/
│   ├── Home/Index.cshtml       # Full portfolio single page
│   ├── Admin/Login.cshtml      # Admin login
│   ├── Admin/Index.cshtml      # Admin dashboard
│   └── Shared/_Layout.cshtml   # Shared head/scripts
├── wwwroot/
│   ├── css/portfolio.css       # All custom styles
│   ├── js/portfolio.js         # All custom scripts
│   ├── images/profile.jpg      # ← ADD YOUR PHOTO HERE
│   └── files/cv.pdf            # ← ADD YOUR CV HERE
├── appsettings.json            # Connection string + admin password
└── Program.cs                  # App startup + DB init
```

---

## Deployment (Azure)

1. Right-click project in Visual Studio → Publish → Azure App Service
2. Add a SQL Server connection string in Azure App Configuration
3. The app will auto-migrate on startup

---

## Customization

All color variables are in `wwwroot/css/portfolio.css` under `:root {}`.
To change the accent colors, update `--violet` and `--teal`.

---

Built by Muhammad Hunain | [GitHub](https://github.com/muhammadhunain20) | [LinkedIn](https://www.linkedin.com/in/muhammad-hunain-9966b631a)
