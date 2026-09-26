
# Gift of the Givers Foundation — Platform

[![Build Status](https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_apis/build/status/YOUR-PIPELINE-ID?branchName=main)](https://dev.azure.com/YOUR-ORG/GiftOfTheGivers/_build/latest?definitionId=YOUR-PIPELINE-ID&branchName=main)

A **humanitarian aid platform** built on **ASP.NET Core 8**, **Azure Functions**, **SQL Server**, and **Azure DevOps CI/CD**. The platform supports emergency disaster-relief operations for Gift of the Givers Foundation by enabling donations, volunteer coordination, and employee-managed relief updates.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Solution Structure](#solution-structure)
- [Technology Stack](#technology-stack)
- [Features](#features)
- [Screenshots](#screenshots)
- [Getting Started](#getting-started)
- [Running the Solution](#running-the-solution)
- [Running Tests](#running-tests)
- [CI/CD](#cicd)
- [Security](#security)
- [Contributors](#contributors)
- [Links](#links)

---

## Overview

Gift of the Givers Foundation is a South African humanitarian organisation that delivers emergency assistance and disaster relief to vulnerable communities. This platform digitises and streamlines their core operations:

- **Donations** — anonymous and registered donors, multiple currencies (ZAR, USD, EUR), multiple frequencies (once-off, monthly, quarterly, annually).
- **Volunteers** — capture volunteer registrations with skills and availability.
- **Relief Updates** — employees post real-time updates on active projects, visible to the public.
- **Serverless Donation Processing** — an Azure Function validates donation payloads and issues tax-certificate numbers.

---

## Architecture

```
+---------------------------------------------------------------+
|                          Azure DevOps                          |
|  +----------+    +-------------+    +--------------------+    |
|  |  Repos   |--->|  Pipelines  |--->|  Azure Artifacts   |    |
|  |  (Git)   |    |  (YAML CI)  |    |  (NuGet Package)   |    |
|  +----------+    +-------------+    +--------------------+    |
+---------------------------------------------------------------+
                             |
               +-------------+-------------+
               |             |             |
               v             v             v
       +------------+  +------------+  +------------+
       |  Web App   |  |  Functions |  |  Helpers   |
       | ASP.NET 8  |  |  (Azure)   |  | (NuGet lib)|
       |    MVC     |  |  HTTP Trig |  |            |
       +------------+  +------------+  +------------+
               |             |
               v             v
       +------------+  +--------------+
       | SQL Server |  | App Insights |
       |  EF Core   |  |   Logging    |
       +------------+  +--------------+
```

---

## Solution Structure

| Project | Type | Purpose |
|---------|------|---------|
| `GiftOfTheGivers.Web` | ASP.NET Core MVC | Web application — Identity, Donations, Volunteers, Employee dashboard |
| `GiftOfTheGivers.Functions` | Azure Functions (isolated) | Serverless donation-processing HTTP trigger |
| `GiftOfTheGivers.Helpers` | Class Library (NuGet) | Reusable business logic — tax certificates, currency formatting, donation calculations |
| `GiftOfTheGivers.Tests` | xUnit | Automated unit tests |

---

## Technology Stack

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

## Features

### Identity & Authorisation
- ASP.NET Core Identity with role-based access
- **Employee** role: view donations, view volunteers, post relief updates
- **Donor** role: make donations, view own history
- **Anonymous** donations supported without registration
- Server-side authorisation enforced (not just hidden links)

### Donation Management
- One-off and recurring donations
- Multiple currencies: ZAR, USD, EUR
- Anonymous or registered donors
- Automatic dummy tax-certificate generation
- Validation and confirmation flow

### Volunteer Management
- Public registration form (name, skills, availability, contact)
- Employee-only view of all registrations

### Relief Updates
- Employees post updates to the public site
- Categorised by project
- Live on the home page

### Serverless
- `POST /api/donations/process` — Azure Function validates donations, calculates annualised totals, generates tax-certificate numbers, and returns structured responses with proper HTTP status codes.

---
### Test Accounts

The database is seeded on first run with two development accounts, one for each role. Use these to test the Employee and Donor experiences.

| Role | Email | Password | Access |
|------|-------|----------|--------|
| **Employee** | `employee@giftofthegivers.org` | `Employee@123` | Employee dashboard, view all donations, view all volunteers, post relief updates |
| **Donor** | `donor@giftofthegivers.org` | `Donor@123` | Make donations, view own donation history |

> **⚠️ Security note:** These credentials exist **only for local development and demonstration**. In a production deployment they would be removed, and roles would be assigned manually by a system administrator. Never commit real credentials to source control.

**How to log in:**

1. Navigate to `/Identity/Account/Login`
2. Enter the Employee or Donor email and password above
3. After logging in as **Employee**, an **Employee** link appears in the navbar
4. After logging in as **Donor**, the Employee link is hidden (authorisation enforced)

**Registration:** New donor accounts can be created via `/Identity/Account/Register`. New accounts are assigned the **Donor** role by default.

## Screenshots

A visual walkthrough of the Gift of the Givers platform, from public-facing pages through to employee tooling and automated tests.

### 1. Public Home Page

The landing page presents the mission, live statistics (total donations, amount raised, active volunteers, active projects), and the three most recent relief updates posted by employees.

![Public home page showing the hero section, live statistics cards, and the latest relief updates feed](https://github.com/user-attachments/assets/9dc5ab29-7476-4c70-ba9c-37d9d6e6d121)

### 2. Donation Workflow

#### 2.1 Donation Form

The public donation form supports anonymous or named donations, multiple currencies (ZAR, USD, EUR), multiple frequencies (Once-off, Monthly, Quarterly, Annually), and client- and server-side validation.

![Donation form with anonymous checkbox, donor details fields, amount input, and currency and frequency dropdowns](https://github.com/user-attachments/assets/f6e1451a-3ad1-4caf-b0e0-18c7098c111c)

#### 2.2 Validation Error Handling

Server-side validation rejects invalid input with clear, user-friendly messages. In this capture, an invalid donation amount is caught and displayed inline.

![Donation form showing a red validation error message below the amount field](https://github.com/user-attachments/assets/afe159b8-3917-4f9e-a8e8-5fceac9eb973)

#### 2.3 Anonymous Donation Confirmation

After a successful anonymous donation, the donor receives a confirmation page displaying the amount and currency, the donation frequency, a generated tax-certificate number (e.g., `GOTG-2026-000123`), and the date and time of the transaction.

![Confirmation page for an anonymous donation showing the tax certificate number and donation details](https://github.com/user-attachments/assets/fdaacb88-6eb2-4fce-bfca-5305df50b044)

### 3. Volunteer Registration

#### 3.1 Volunteer Signup Form

Public registration form capturing full name, skills, availability, and optional contact details — persisted to the database for employee review.

![Volunteer registration form with fields for full name, skills, availability, email, and phone](https://github.com/user-attachments/assets/9319cf20-2174-44f3-bcef-5e0985c15255)

### 4. Identity & Authentication

ASP.NET Core Identity provides the authentication backbone, with Employee and Donor roles enforced server-side.

#### 4.1 Login

![Login page with email and password fields and a remember-me checkbox](https://github.com/user-attachments/assets/135e6e42-be00-459a-bb32-8daf185205cd)

#### 4.2 Register

![Registration page for new donors with email and password fields](https://github.com/user-attachments/assets/d623c1c3-eecb-4605-a301-d6531e43ac22)

### 5. Employee Area

The employee-only section is protected by `[Authorize(Roles = "Employee")]`. Unauthenticated users and donors are denied access — proven by the access-denied redirect on direct navigation attempts.

#### 5.1 Employee Dashboard (Overview)

Displays live counts of donations and volunteers, plus quick access to detailed lists and the "Post Relief Update" action.

![Employee dashboard showing donation count, volunteer count, action cards, and recent relief updates table](https://github.com/user-attachments/assets/f23fd9c5-feb0-4a76-b100-2e5bf0167786)

#### 5.2 Employee Dashboard (Updates Feed)

Detailed view of recent relief updates posted by employees, including timestamps and project associations.

![Employee dashboard with a table of recent relief updates](https://github.com/user-attachments/assets/004ec20e-3881-4317-849b-d62de8efdab2)

### 6. Solution Structure & Automated Tests

#### 6.1 Solution Structure in Visual Studio

The complete solution consists of four projects following strict separation of concerns:

| Project | Role |
|---------|------|
| **GiftOfTheGivers.Web** | ASP.NET Core MVC web application |
| **GiftOfTheGivers.Functions** | Azure Function (isolated worker, HTTP trigger) |
| **GiftOfTheGivers.Helpers** | Reusable class library (published to Azure Artifacts) |
| **GiftOfTheGivers.Tests** | xUnit automated test suite |

![Visual Studio Solution Explorer showing all four projects: Web, Functions, Helpers, Tests](https://github.com/user-attachments/assets/f2258500-0f04-404f-9bf9-1b24ece73784)

#### 6.2 Unit Tests Passing

21 unit tests cover the helper library:

- `TaxCertificateFormatter` — format, padding, year defaults, error handling
- `CurrencyFormatter` — ZAR/USD/EUR formatting, case-insensitivity, unsupported currencies
- `DonationCalculator` — amount validation, annualised totals, invalid frequency handling

All tests pass on every push via Azure Pipelines.

![Terminal output showing 21 passing xUnit tests](https://github.com/user-attachments/assets/7a33805c-f74d-4533-8ce4-f62344ed7dba)

### 7. Additional Build & Pipeline Evidence

#### 7.1 Continuous Integration Build Output

![Azure Pipelines build output showing Restore, Build, and Test stages](https://github.com/user-attachments/assets/fcfff57e-8f8a-45bf-b9c2-046c241e6fb2)

#### 7.2 Project Structure Reference

![Folder structure of the solution](https://github.com/user-attachments/assets/1b660dbc-2d68-41d0-8bc6-f482afc088bd)

#### 7.3 Build Verification

![Screenshot confirming build verification](https://github.com/user-attachments/assets/e15bfa1c-0bca-4422-9646-84d97ddd73f8)

### Screenshot Index

| # | Section | Purpose |
|---|---------|---------|
| 1 | Public Home Page | Shows live stats + relief updates |
| 2.1 | Donation Form | All donation options visible |
| 2.2 | Validation Error | Server-side validation enforced |
| 2.3 | Anonymous Confirmation | Tax certificate generated |
| 3.1 | Volunteer Form | Registration flow captured |
| 4.1 | Login | Identity login page |
| 4.2 | Register | Identity registration page |
| 5.1 | Employee Dashboard Overview | Employee-only page works |
| 5.2 | Employee Dashboard Updates | Relief updates list |
| 6.1 | Solution Structure | All 4 projects visible |
| 6.2 | Tests | 21/21 automated tests passing |
| 7.1 | Build Output | CI pipeline result |
| 7.2 | Project Structure | Folder layout |
| 7.3 | Build Verification | Final build sanity check |

---

## Getting Started

### Prerequisites

- .NET 8 SDK — [download](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (17.8+) or VS Code with C# extension
- SQL Server or LocalDB
- Azure Functions Core Tools v4 — `winget install Microsoft.Azure.FunctionsCoreTools`
- Git

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

### Restore, Build and Migrate

```bash
cd ..
dotnet restore
dotnet build -c Release
dotnet ef database update -p GiftOfTheGivers.Web -s GiftOfTheGivers.Web
```

---

## Running the Solution

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

## Running Tests

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

## CI/CD

The solution uses Azure Pipelines with a YAML definition (`azure-pipelines.yml`) that:

1. Restore — installs .NET 8 SDK and restores NuGet packages
2. Build — compiles the full solution in Release mode
3. Test — runs the xUnit test suite and publishes results

The pipeline auto-triggers on any push to `main` or a `feature/*` branch.

### Build Badge

The status badge at the top of this README reflects the current state of the `main` branch.

---

## Security

- No secrets in source code — connection strings use User Secrets (local) and Azure Application Settings (cloud)
- Secrets never committed — `local.settings.json` and `appsettings.Development.json` are gitignored
- Server-side authorisation — `[Authorize(Roles = "Employee")]` enforced in controllers, not just hidden links
- HTTPS enforced in non-development environments
- No sensitive information in logs — only donation IDs, currencies, and formatted amounts are logged

---

## Contributors

| Contributor | Email | Role |
|-------------|-------|------|
| L. More | st10451027@rcconnect.edu.za | Solution architecture, Web app, Azure Functions, CI/CD, Helpers library |

> **Note:** The feature-branch workflow, Pull Requests, and merge-conflict evidence are documented in the `Evidence/Phase5_Azure_Repos/` folder.

---

## Links

- Azure DevOps: https://dev.azure.com/YOUR-ORG/GiftOfTheGivers
- Gift of the Givers Foundation: https://www.giftofthegivers.org/
- Issues & Feedback: Raise a work item in Azure DevOps Boards

---

*"Bringing Hope to Communities"*
```

---
