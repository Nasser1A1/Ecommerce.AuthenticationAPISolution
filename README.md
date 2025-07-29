# 🛡️ Authentication API Microservice

This microservice is responsible for **creating and authenticating users**. It handles user registration, login, and issues JWT tokens for secure communication between clients and other microservices.

---

## ⚠️ Dependency

> This service **requires** the [Shared Library Solution](https://github.com/Nasser1A1/SharedLibrarySolution) to run correctly.

The shared library provides:
- Global exception handling
- Logging setup using Serilog
- Middleware configuration
- Common service registration extensions
- Standardized response models

Make sure to clone and reference the Shared Library project before running this service.

---

## 🚀 Getting Started

### 1. Clone the Authentication Service

```bash
git clone https://github.com/your-username/AuthenticationApi.git
cd AuthenticationApi
```

### 2. Clone and Build the Shared Library

```bash
git clone https://github.com/Nasser1A1/SharedLibrarySolution.git
cd SharedLibrarySolution
dotnet build
```

Then, add a **project reference** to the shared library from the Authentication API project.

### 3. Update Configuration

In `appsettings.json`, update the following sections:

```json
"Jwt": {
  "Key": "your-secret-key",
  "Issuer": "your-app",
  "Audience": "your-app-users",
  "DurationInMinutes": 60
},
"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server or PostgreSQL connection string"
}
```

### 4. Run the Project

```bash
dotnet run
```

The API will start on `https://localhost:5001` or `http://localhost:5000` by default.

---

## 🔐 Endpoints

| Method | Route              | Description             |
|--------|-------------------|-------------------------|
| POST   | `/api/auth/register` | Register a new user     |
| POST   | `/api/auth/login`    | Authenticate a user and receive a JWT |

---

## 🧪 Testing

Use Swagger at:

```
https://localhost:5000/swagger
```

Or test using Postman by sending requests to the routes above.

---

## 🧰 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT Authentication
- Serilog
- Swagger

---

## 📄 License

This project is open source under the MIT License.
