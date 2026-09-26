
# 🌍 Gift of the Givers Foundation — Platform

[![Build Status](https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_apis/build/status/YOUR-PIPELINE-ID?branchName=main)](https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_build/latest?definitionId=YOUR-PIPELINE-ID&branchName=main)

A **humanitarian aid platform** built on **ASP.NET Core 8**, **Azure Functions**, **SQL Server**, and **Azure DevOps CI/CD**. The platform supports emergency disaster-relief operations for Gift of the Givers Foundation by enabling donations, volunteer coordination, and employee-managed relief updates.

---

## 📖 Table of Contents

- [Overview](#-overview)
- [Architecture](#-architecture)
- [Solution Structure](#-solution-structure)
- [Technology Stack](#-technology-stack)
- [Features](#-features)
- [Screenshots](#-screenshots)
- [Getting Started](#-getting-started)
- [Running the Solution](#-running-the-solution)
- [Running Tests](#-running-tests)
- [CI/CD](#-cicd)
- [Security](#-security)
- [Contributors](#-contributors)

---

## 🎯 Overview

Gift of the Givers Foundation is a South African humanitarian organisation that delivers emergency assistance and disaster relief to vulnerable communities. This platform digitises and streamlines their core operations:

- **Donations** — support anonymous and registered donors with multiple currencies (ZAR, USD, EUR) and frequencies (once-off, monthly, quarterly, annually).
- **Volunteers** — capture volunteer registrations with skills and availability.
- **Relief Updates** — allow employees to post real-time updates on active projects, visible to the public.
- **Serverless Donation Processing** — an Azure Function validates and processes donation payloads and issues tax-certificate numbers.

---

## 🏗 Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                       Azure DevOps                          │
│  ┌──────────┐    ┌────────────┐    ┌──────────────────┐    │
│  │  Repos   │──▶│  Pipelines │──▶│ Azure Artifacts  │    │
│  │ (Git)    │    │  (YAML CI) │    │ (NuGet Package)  │    │
│  └──────────┘    └────────────┘    └──────────────────┘    │
└─────────────────────────────────────────────────────────────┘
                            │
              ┌─────────────┼─────────────┐
              ▼             ▼             ▼
      ┌────────────┐  ┌────────────┐  ┌────────────┐
      │  Web App   │  │  Functions │  │  Helpers   │
      │ ASP.NET 8  │  │  (Azure)   │  │ (NuGet lib)│
      │    MVC     │  │  HTTP Trig │  │            │
      └────────────┘  └────────────┘  └────────────┘
              │             │
              ▼             ▼
      ┌────────────┐  ┌────────────┐
      │ SQL Server │  │ App Insights│
      │ EF Core    │  │ Logging     │
      └────────────┘  └────────────┘
```

---

## 📁 Solution Structure

| Project | Type | Purpose |
|---------|------|---------|
| `GiftOfTheGivers.Web` | ASP.NET Core MVC | Web application — Identity, Donations, Volunteers, Employee dashboard |
| `GiftOfTheGivers.Functions` | Azure Functions (isolated) | Serverless donation-processing HTTP trigger |
| `GiftOfTheGivers.Helpers` | Class Library (NuGet) | Reusable business logic — tax certificates, currency formatting, donation calculations |
| `GiftOfTheGivers.Tests` | xUnit | Automated unit tests |

---

## 🛠 Technology Stack

| Layer | Technology |
|-------|-----------|
| **Language** | C# 12 |
| **Runtime** | .NET 8 |
| **Web framework** | ASP.NET Core MVC |
| **UI** | Bootstrap 5, Razor |
| **Authentication** | ASP.NET Core Identity (Employee + Donor roles) |
| **Database** | SQL Server (LocalDB for dev, Azure SQL for prod) |
| **ORM** | Entity Framework Core |
| **Serverless** | Azure Functions (isolated worker, HTTP trigger) |
| **Package management** | NuGet + Azure Artifacts |
| **CI/CD** | Azure Pipelines (YAML) |
| **Source control** | Azure Repos (Git, feature-branch workflow) |
| **Logging** | `Microsoft.Extensions.Logging`, Application Insights |

---

## ✨ Features

### 🔐 Identity & Authorisation
- ASP.NET Core Identity with role-based access
- **Employee** role: view donations, view volunteers, post relief updates
- **Donor** role: make donations, view own history
- **Anonymous** donations supported without registration
- Server-side authorisation enforced (not just hidden links)

### 💰 Donation Management
- One-off and recurring donations
- Multiple currencies: ZAR, USD, EUR
- Anonymous or registered donors
- Automatic dummy tax-certificate generation
- Validation and confirmation flow

### 🤝 Volunteer Management
- Public registration form (name, skills, availability, contact)
- Employee-only view of all registrations

### 📰 Relief Updates
- Employees post updates to the public site
- Categorised by project
- Live on the home page

### ⚡ Serverless
- `POST /api/donations/process` — Azure Function validates donations, calculates annualised totals, generates tax-certificate numbers, and returns structured responses with proper HTTP status codes.

---

## 📸 Screenshots

### 🏠 Public Site

#### Home Page
![Homepage — hero, stats, and latest relief updates](https://github.com/user-attachments/assets/d645dd9a-36b4-46e7-ab12-1f6cdd212036)

---

### 💰 Donation Workflow

#### Donation Form
![Donation form with anonymous option, amount, currency, and frequency](https://github.com/user-attachments/assets/9e0e9a04-c6ec-4e01-a76d-7b0f09c5edcf)

#### Donation Validation Error
![Server-side validation rejecting invalid input](https://github.com/user-attachments/assets/84476dbe-cd9e-4379-ba03-02df68104d6b)

#### Anonymous Donation Confirmation
![Successful anonymous donation with tax certificate number](https://github.com/user-attachments/assets/79a7e57a-100a-4ae6-b15d-8ec111e0ac62)

---

### 🤝 Volunteer Registration

#### Volunteer Signup
![Volunteer registration form](https://github.com/user-attachments/assets/c36c0f87-41b2-4e1d-ae8d-e913c89ee01c)

#### Thank-You Confirmation
![Confirmation page after submitting volunteer registration](https://github.com/user-attachments/assets/c0a2dcd4-aefb-4a97-b03c-c9160d7936c4)

---

### 🔐 Identity & Authentication

#### Login
![ASP.NET Core Identity login page](https://github.com/user-attachments/assets/f5b7c0f3-bbaa-4416-917b-6636ebb00c0a)

#### Register
![New account registration](https://github.com/user-attachments/assets/b5428601-dce3-4e84-8355-2532c92db8ee)

---

### 👔 Employee Area

#### Employee Dashboard — Overview
![Employee dashboard with donation and volunteer counts](https://github.com/user-attachments/assets/fc6ff3c6-0e96-4cf6-b1ae-8840a4fa1396)

#### Employee Dashboard — Relief Updates
![Employee dashboard showing recent relief updates](https://github.com/user-attachments/assets/34c6ca27-738d-4568-bf79-17580b75e53b)

---

### 🧱 Solution & Tests

#### Solution Structure in Visual Studio
![Solution Explorer showing all four projects](https://github.com/user-attachments/assets/349f10a6-96af-4f42-9a71-93941834a2f8)

#### Unit Tests Passing
![21 xUnit tests passing](https://github.com/user-attachments/assets/7a20cf43-7693-4dea-a537-c65360948e87)

---

### 📎 Additional Screenshots

#### Build / Pipeline Output
![Build output](https://github.com/user-attachments/assets/c23fdf4f-a766-4cec-b5f6-7721c8348aa0)

#### Project Structure
![Project structure](https://github.com/user-attachments/assets/986a5892-dda2-4f93-b31f-3b814379cd04)

---

## 🚀 Getting Started

### Prerequisites
- **.NET 8 SDK** — [download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022** (17.8+) or **VS Code** with C# extension
- **SQL Server** or **LocalDB**
- **Azure Functions Core Tools v4** — `winget install Microsoft.Azure.FunctionsCoreTools`
- **Git**

### Clone the Repository

```bash
git clone https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_git/GiftOfTheGivers
cd GiftOfTheGivers
```

### Configure Secrets (User Secrets — never committed)

```bash
cd GiftOfTheGivers.Web
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=GiftOfTheGiversDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

### Restore, Build & Migrate

```bash
cd ..
dotnet restore
dotnet build -c Release
dotnet ef database update -p GiftOfTheGivers.Web -s GiftOfTheGivers.Web
```

---

## 🏃 Running the Solution

### Web Application

```bash
cd GiftOfTheGivers.Web
dotnet run
```

Browse to `https://localhost:7001` (or the URL printed in the terminal).

### Azure Function (local)

```bash
cd GiftOfTheGivers.Functions
func start
```

The HTTP trigger is available at `http://localhost:7071/api/donations/process`.

### Test the Function

```bash
curl -X POST http://localhost:7071/api/donations/process \
  -H "Content-Type: application/json" \
  -d '{"donorName":"John Smith","amount":250,"currency":"ZAR","frequency":"Monthly","isAnonymous":false}'
```

**Expected response:**

```json
{
  "success": true,
  "taxCertificateNumber": "GOTG-2026-000123",
  "formattedAmount": "R 250.00",
  "annualisedTotal": 3000,
  "message": "Donation processed successfully."
}
```

---

## 🧪 Running Tests

```bash
dotnet test GiftOfTheGivers.Tests/GiftOfTheGivers.Tests.csproj
```

Expected output:

```
Passed!  - Failed: 0, Passed: 21, Skipped: 0, Total: 21
```

Coverage includes:

- `TaxCertificateFormatter` — format, padding, year defaults, error handling
- `CurrencyFormatter` — ZAR/USD/EUR formatting, case-insensitivity, unsupported currencies
- `DonationCalculator` — amount validation, annualised totals, invalid frequency handling

---

## 🔄 CI/CD

The solution uses **Azure Pipelines** with a YAML definition (`azure-pipelines.yml`) that:

1. **Restore** — installs .NET 8 SDK and restores NuGet packages
2. **Build** — compiles the full solution in Release mode
3. **Test** — runs the xUnit test suite and publishes results

The pipeline auto-triggers on any push to `main` or a `feature/*` branch.

### Build Badge

The status badge at the top of this README reflects the current state of the `main` branch.

---

## 🔒 Security

- **No secrets in source code** — connection strings use **User Secrets** (local) and **Azure Application Settings** (cloud)
- **Secrets never committed** — `local.settings.json` and `appsettings.Development.json` are gitignored
- **Server-side authorisation** — `[Authorize(Roles = "Employee")]` enforced in controllers, not just hidden links
- **HTTPS** enforced in non-development environments
- **No sensitive information in logs** — only donation IDs, currencies, and formatted amounts are logged

---

## 👥 Contributors

| Contributor | Email | Role |
|-------------|-------|------|
| **L. More** | st10451027@rcconnect.edu.za | Solution architecture, Web app, Azure Functions, CI/CD, Helpers library |

> **Note:** The feature-branch workflow, Pull Requests, and merge-conflict evidence are documented in the `Evidence/Phase5_Azure_Repos/` folder.

---

## 🔗 Links

- **Azure DevOps:** https://dev.azure.com/YOUR-ORG/GiftOfTheGivers
- **Gift of the Givers Foundation:** https://www.giftofthegivers.org/
- **Issues & Feedback:** Raise a work item in Azure DevOps Boards

---

*"Bringing Hope to Communities"*
```

---

## 🚀 Apply It in One Command

Paste this block into PowerShell:

```powershell
cd C:\Dev\GiftOfTheGivers
# Paste the full README content above into a variable
$readme = @'
# 🌍 Gift of the Givers Foundation — Platform

[![Build Status](https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_apis/build/status/YOUR-PIPELINE-ID?branchName=main)](https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_build/latest?definitionId=YOUR-PIPELINE-ID&branchName=main)

A **humanitarian aid platform** built on **ASP.NET Core 8**, **Azure Functions**, **SQL Server**, and **Azure DevOps CI/CD**. The platform supports emergency disaster-relief operations for Gift of the Givers Foundation by enabling donations, volunteer coordination, and employee-managed relief updates.

---

## 📖 Table of Contents

- [Overview](#-overview)
- [Architecture](#-architecture)
- [Solution Structure](#-solution-structure)
- [Technology Stack](#-technology-stack)
- [Features](#-features)
- [Screenshots](#-screenshots)
- [Getting Started](#-getting-started)
- [Running the Solution](#-running-the-solution)
- [Running Tests](#-running-tests)
- [CI/CD](#-cicd)
- [Security](#-security)
- [Contributors](#-contributors)

---

## 🎯 Overview

Gift of the Givers Foundation is a South African humanitarian organisation that delivers emergency assistance and disaster relief to vulnerable communities. This platform digitises and streamlines their core operations:

- **Donations** — support anonymous and registered donors with multiple currencies (ZAR, USD, EUR) and frequencies (once-off, monthly, quarterly, annually).
- **Volunteers** — capture volunteer registrations with skills and availability.
- **Relief Updates** — allow employees to post real-time updates on active projects, visible to the public.
- **Serverless Donation Processing** — an Azure Function validates and processes donation payloads and issues tax-certificate numbers.

---

## 🏗 Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                       Azure DevOps                          │
│  ┌──────────┐    ┌────────────┐    ┌──────────────────┐    │
│  │  Repos   │──▶│  Pipelines │──▶│ Azure Artifacts  │    │
│  │ (Git)    │    │  (YAML CI) │    │ (NuGet Package)  │    │
│  └──────────┘    └────────────┘    └──────────────────┘    │
└─────────────────────────────────────────────────────────────┘
                            │
              ┌─────────────┼─────────────┐
              ▼             ▼             ▼
      ┌────────────┐  ┌────────────┐  ┌────────────┐
      │  Web App   │  │  Functions │  │  Helpers   │
      │ ASP.NET 8  │  │  (Azure)   │  │ (NuGet lib)│
      │    MVC     │  │  HTTP Trig │  │            │
      └────────────┘  └────────────┘  └────────────┘
              │             │
              ▼             ▼
      ┌────────────┐  ┌────────────┐
      │ SQL Server │  │ App Insights│
      │ EF Core    │  │ Logging     │
      └────────────┘  └────────────┘
```

---

## 📁 Solution Structure

| Project | Type | Purpose |
|---------|------|---------|
| `GiftOfTheGivers.Web` | ASP.NET Core MVC | Web application — Identity, Donations, Volunteers, Employee dashboard |
| `GiftOfTheGivers.Functions` | Azure Functions (isolated) | Serverless donation-processing HTTP trigger |
| `GiftOfTheGivers.Helpers` | Class Library (NuGet) | Reusable business logic — tax certificates, currency formatting, donation calculations |
| `GiftOfTheGivers.Tests` | xUnit | Automated unit tests |

---

## 🛠 Technology Stack

| Layer | Technology |
|-------|-----------|
| **Language** | C# 12 |
| **Runtime** | .NET 8 |
| **Web framework** | ASP.NET Core MVC |
| **UI** | Bootstrap 5, Razor |
| **Authentication** | ASP.NET Core Identity (Employee + Donor roles) |
| **Database** | SQL Server (LocalDB for dev, Azure SQL for prod) |
| **ORM** | Entity Framework Core |
| **Serverless** | Azure Functions (isolated worker, HTTP trigger) |
| **Package management** | NuGet + Azure Artifacts |
| **CI/CD** | Azure Pipelines (YAML) |
| **Source control** | Azure Repos (Git, feature-branch workflow) |
| **Logging** | Microsoft.Extensions.Logging, Application Insights |

---

## ✨ Features

### 🔐 Identity & Authorisation
- ASP.NET Core Identity with role-based access
- **Employee** role: view donations, view volunteers, post relief updates
- **Donor** role: make donations, view own history
- **Anonymous** donations supported without registration
- Server-side authorisation enforced (not just hidden links)

### 💰 Donation Management
- One-off and recurring donations
- Multiple currencies: ZAR, USD, EUR
- Anonymous or registered donors
- Automatic dummy tax-certificate generation
- Validation and confirmation flow

### 🤝 Volunteer Management
- Public registration form (name, skills, availability, contact)
- Employee-only view of all registrations

### 📰 Relief Updates
- Employees post updates to the public site
- Categorised by project
- Live on the home page

### ⚡ Serverless
- POST /api/donations/process — Azure Function validates donations, calculates annualised totals, generates tax-certificate numbers, and returns structured responses with proper HTTP status codes.

---

## 📸 Screenshots

### 🏠 Public Site
#### Home Page
![Homepage — hero, stats, and latest relief updates](https://github.com/user-attachments/assets/d645dd9a-36b4-46e7-ab12-1f6cdd212036)

### 💰 Donation Workflow
#### Donation Form
![Donation form with anonymous option, amount, currency, and frequency](https://github.com/user-attachments/assets/9e0e9a04-c6ec-4e01-a76d-7b0f09c5edcf)

#### Donation Validation Error
![Server-side validation rejecting invalid input](https://github.com/user-attachments/assets/84476dbe-cd9e-4379-ba03-02df68104d6b)

#### Anonymous Donation Confirmation
![Successful anonymous donation with tax certificate number](https://github.com/user-attachments/assets/79a7e57a-100a-4ae6-b15d-8ec111e0ac62)

### 🤝 Volunteer Registration
#### Volunteer Signup
![Volunteer registration form](https://github.com/user-attachments/assets/c36c0f87-41b2-4e1d-ae8d-e913c89ee01c)

#### Thank-You Confirmation
![Confirmation page after submitting volunteer registration](https://github.com/user-attachments/assets/c0a2dcd4-aefb-4a97-b03c-c9160d7936c4)

### 🔐 Identity & Authentication
#### Login
![ASP.NET Core Identity login page](https://github.com/user-attachments/assets/f5b7c0f3-bbaa-4416-917b-6636ebb00c0a)

#### Register
![New account registration](https://github.com/user-attachments/assets/b5428601-dce3-4e84-8355-2532c92db8ee)

### 👔 Employee Area
#### Employee Dashboard — Overview
![Employee dashboard with donation and volunteer counts](https://github.com/user-attachments/assets/fc6ff3c6-0e96-4cf6-b1ae-8840a4fa1396)

#### Employee Dashboard — Relief Updates
![Employee dashboard showing recent relief updates](https://github.com/user-attachments/assets/34c6ca27-738d-4568-bf79-17580b75e53b)

### 🧱 Solution & Tests
#### Solution Structure in Visual Studio
![Solution Explorer showing all four projects](https://github.com/user-attachments/assets/349f10a6-96af-4f42-9a71-93941834a2f8)

#### Unit Tests Passing
![21 xUnit tests passing](https://github.com/user-attachments/assets/7a20cf43-7693-4dea-a537-c65360948e87)

### 📎 Additional Screenshots
#### Build / Pipeline Output
![Build output](https://github.com/user-attachments/assets/c23fdf4f-a766-4cec-b5f6-7721c8348aa0)

#### Project Structure
![Project structure](https://github.com/user-attachments/assets/986a5892-dda2-4f93-b31f-3b814379cd04)

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 (17.8+) or VS Code
- SQL Server / LocalDB
- Azure Functions Core Tools v4
- Git

### Clone
```bash
git clone https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_git/GiftOfTheGivers
cd GiftOfTheGivers
```

### Configure Secrets
```bash
cd GiftOfTheGivers.Web
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=GiftOfTheGiversDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

### Restore, Build, Migrate
```bash
cd ..
dotnet restore
dotnet build -c Release
dotnet ef database update -p GiftOfTheGivers.Web -s GiftOfTheGivers.Web
```

---

## 🏃 Running the Solution

### Web App
```bash
cd GiftOfTheGivers.Web
dotnet run
```

### Azure Function (local)
```bash
cd GiftOfTheGivers.Functions
func start
```

### Test the Function
```bash
curl -X POST http://localhost:7071/api/donations/process \
  -H "Content-Type: application/json" \
  -d '{"donorName":"John Smith","amount":250,"currency":"ZAR","frequency":"Monthly","isAnonymous":false}'
```

Expected response:
```json
{
  "success": true,
  "taxCertificateNumber": "GOTG-2026-000123",
  "formattedAmount": "R 250.00",
  "annualisedTotal": 3000,
  "message": "Donation processed successfully."
}
```

---

## 🧪 Running Tests

```bash
dotnet test GiftOfTheGivers.Tests/GiftOfTheGivers.Tests.csproj
```

Expected:
```
Passed!  - Failed: 0, Passed: 21, Skipped: 0, Total: 21
```

---

## 🔄 CI/CD

Azure Pipelines with `azure-pipelines.yml`:
1. **Restore** — .NET 8 SDK + NuGet packages
2. **Build** — full solution, Release configuration
3. **Test** — xUnit with published results

Auto-triggers on push to `main` and `feature/*`.

---

## 🔒 Security

- User Secrets for local dev, Azure Application Settings for cloud
- No secrets committed (`local.settings.json` and `appsettings.Development.json` gitignored)
- Server-side authorisation via `[Authorize(Roles="Employee")]`
- HTTPS enforced in production
- No sensitive info in logs

---

## 👥 Contributors

| Contributor | Email | Role |
|-------------|-------|------|
| **L. More** | st10451027@rcconnect.edu.za | Solution architecture, Web app, Azure Functions, CI/CD, Helpers library |

---

## 🔗 Links

- **Azure DevOps:** https://dev.azure.com/YOUR-ORG/GiftOfTheGivers
- **Gift of the Givers Foundation:** https://www.giftofthegivers.org/

---

*"Bringing Hope to Communities"*
'@

$readme | Out-File -Encoding utf8 README.md
Write-Host "✓ README.md created successfully" -ForegroundColor Green
```

---
