# Fitness Tracker Application

A full-stack fitness logging and analytics dashboard. This application allows users to record their daily training sessions, track workout duration, evaluate training metrics like intensity and fatigue, and view weekly breakdown performance summary over a monthly timeline.

## Architecture Overview

The system is built using a decoupled client-server architecture:

- **Backend:** .NET Web API following Clean Architecture patterns (Controllers ➔ Services ➔ Repositories) powered by Entity Framework Core and an SQL database.
- **Frontend:** Angular standalone SPA utilizing reactive programming patterns (RxJS `BehaviorSubject` streams, and modern Signals state management) styled with Tailwind CSS.

## Prerequisites

Before running either application, ensure you have the following installed on your machine:

- [.NET SDK (Version 8.0 or later)](https://dotnet.microsoft.com/download)
- [Node.js (LTS Version)](https://nodejs.org/) & npm
- [Angular CLI](https://angular.dev/tools/cli) (`npm install -g @angular/cli`)
- Your preferred SQL Database Engine (SQL Server, PostgreSQL, or SQLite local dev instance)

## Backend Setup & Database Migrations

### 1. Configure the Environment

Navigate to your backend web api project folder and locate the `appsettings.json` file. Update the database connection string to point to your local development database instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=exercisetracker.db"
  }
}
```

### 2. Apply Migrations

Run the following commands from the root directory containing your .csproj database context file to generate and apply your database tables:

```bash
dotnet tool install --global dotnet-ef
dotnet build
dotnet ef database update
```

### 3. Run Backend Service

```bash
dotnet run
```

## Frontend Setup

### 1. Install Dependecies

```bash
npm install
```

### 2. Configure API Endpoints

Verify that your frontend environment configuration files point to your exact backend application port. Look for your API environment file located in `src\environments\environment.development.ts`

```TypeScript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5082/api',
};
```

### 3. Run Angular Application

```bash 
ng serve -o
```