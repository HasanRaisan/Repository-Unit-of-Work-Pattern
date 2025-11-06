# 🎓 TeacherStudentAPI

A clean and well-structured **Three-Tier ASP.NET Core Web API** project for managing **Teachers, Students, and Users (with Roles and Authentication)**.  
It follows best practices for **Clean Architecture**, **Separation of Concerns**, and **Dependency Injection**.

---

## 🧱 Project Structure

```
TeacherStudentAPI
|
|-- API
|    |-- Properties
|    |      |-- launchSettings.json
|    |      |
|    |-- Controllers
|    |      |-- StudentController.cs
|    |      |-- TeacherController.cs
|    |      |-- UserController.cs
|    |      |
|    |-- Midlleware
|    |      |-- AdvancedProfilingMiddleware.cs
|    |      |-- ExceptionHandlingMiddleware.cs
|    |      |-- RateLimitingMiddleware.cs
|    |      |
|    |-- packages.cs
|    |-- appsettings.json
|    |-- Program
|    |-- TeacherStudentAPI.http
|    |
|-- Business
|    |-- Configruration
|    |      | -- JwtSettings.cs
|    |-- Constants
|    |      | -- RoleConstants.cs
|    |- - - - DTOs
|    |      | -- Identity
|    |      |       |-- AssignRoleDTO.cs
|    |      |       |-- LoginDTO.cs
|    |      |       |-- RegisterDTO.cs
|    |      | -- Student
|    |      |       |-- StudentDTO.cs
|    |      | -- Teacher
|    |      |       |-- TeacherDTO.cs
|    |- - Extensions
|    |      | -- ServiceCollectionExtensions.cs
|    |- - Mapping
|    |      | -- MappingProfile.cs
|    |- - Result
|    |      | -- AuthResult.cs
|    |      | -- Result.cs
|    |- - Services
|    |      | -- Auth
|    |      |     | -- IAuthService.cs
|    |      |     | -- AuthService.cs
|    |      |     | -- ITokenService.cs
|    |      |     | -- TokenService.cs
|    |      | -- Generic
|    |      |     | -- IGenericService.cs
|    |      | -- Student
|    |      |     | -- IStudent.cs
|    |      |     | -- StudentService.cs
|    |      | -- Teacher
|    |      |     | -- ITeacher.cs
|    |      |     | -- TeacherService.cs
|    |- - Validation
|    |      | -- RegisterDomainValidator.cs
|    |      | -- StudentDomainValidator.cs
|    |      | -- TeacherDomainValidator.cs
|    |- - Domains
|    |      | -- Auth
|    |      |     | -- AssignRoleDomain.cs
|    |      |     | -- LoginDomain.cs
|    |      |     | -- RegisterDomain.cs
|    |      | -- Core
|    |      |     | -- StudentDomain.cs
|    |      |     | -- TeacherDomain.cs
|
|-- Data
|    |-- Data
|    |      |-- Entities
|    |      |      |-- ApplicationRoleEntity.cs
|    |      |      |-- ApplicationUserEntity.cs
|    |      |      |-- StudentEntity.cs
|    |      |      |-- TeacherEntity.cs
|    |      |-- AppDbContext.cs
|    |-- Extensions
|    |      |-- DataAccessExtensions.cs
|    |-- Identity
|    |      |-- ApplicationDbContextSeed.cs
|    |-- Migrations
|    |- - Repositories
|    |      | -- Generic
|    |      |     | -- IRepository.cs
|    |      |     | -- Repository.cs
|    |      | -- Spesific
|    |      |     | -- ITeacherRepository.cs
|    |      |     | -- TeacherRepository.cs
|    |-- UnitOfWork
|    |      |-- IUnitOfWork.cs
|    |      |-- UnitOfWork.cs
```

---

## 🚀 Features

- ✅ Clean three-tier architecture (**API**, **Business**, **Data**)
- ✅ Authentication & Authorization using **ASP.NET Identity**
- ✅ Role-based Access Control (`Admin`, `Teacher`, `Student`)
- ✅ JWT Token Authentication
- ✅ AutoMapper for mapping Entities ↔ DTOs ↔ Domains
- ✅ FluentValidation for input validation
- ✅ Centralized Exception Handling Middleware
- ✅ Rate Limiting & Request Profiling Middleware
- ✅ Repository + Unit of Work pattern for data access

---

## ⚙️ Technologies Used

- **.NET 9 (ASP.NET Core Web API)**
- **Entity Framework Core**
- **FluentValidation**
- **AutoMapper**
- **Identity with Roles**
- **SQL Server**
- **Swagger (API Documentation)**

---

## 🧩 How to Run

1. **Clone the repository**

   ```bash
   git clone https://github.com/HasanRaisan/TeacherStudent-Repository-Unit-of-Work-Pattern.git
   cd TeacherStudentAPI
   ```

2. **Update database connection string** in `API/appsettings.json`

3. **Apply migrations**

   ```bash
   Update-Database
   ```

4. **Run the API**

   ```bash
   dotnet run --project API
   ```

5. **Open Swagger UI**
   👉 https://localhost:5001/swagger

---

## 👨‍💻 Author

**Hasan Raisan**  
📧 hasan.raisann@gmail.com  
💼 Web Developer
