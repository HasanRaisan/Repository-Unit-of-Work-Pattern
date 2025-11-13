# 🎓 TeacherStudentAPI

A **scalable** and **maintainable** **ASP.NET Core Web API** designed using **Clean Architecture** principles, ensuring clear **separation of responsibilities** across all layers.

The project supports **Teacher**, **Student**, and **User Management**, including **Authentication** and **Role-Based Authorization**, and follows best practices for **Separation of Concerns** and **Dependency Injection**.

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
|-- Application
|    |-- Configruration
|    |      | -- JwtSettings.cs
|    |-- Constants
|    |      | -- RoleConstants.cs
|    |- - - - DTOs
|    |      | -- Identity
|    |      |       |-- AssignRoleDTO.cs
|    |      |       |-- AuthResultDTO.cs
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
|    |      | -- Error.cs
|    |      | -- ErrorType.cs
|    |      | -- Result.cs
|    |      | -- ResultFactory.cs
|    |      | -- ResultToActionMapper.cs
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
|    |      | -- AssignRoleDTOValidator.cs
|    |      | -- LoginDTOValidator.cs
|    |      | -- RegisterDTOValidator.cs
|    |      | -- StudentDTOValidator.cs
|    |      | -- TeacherDTOValidator.cs
|    |      |
|-- Domain
|    | -- Entites
|    |      |-- Auth
|    |      |     |-- AssignRoleDomain.cs
|    |      |     | -- LoginDomain.cs
|    |      |     | -- RegisterDomain.cs
|    |      | -- Core
|    |      |     | -- StudentDomain.cs
|    |      |     | -- TeacherDomain.cs
|    |      |
|    | -- Results
|    |      |-- Result
|    |      |
|-- Infrastructure
|    |-- Data
|    |      |-- Entities
|    |      |      |-- ApplicationRoleEntity.cs
|    |      |      |-- ApplicationUserEntity.cs
|    |      |      |-- StudentEntity.cs
|    |      |      |-- TeacherEntity.cs
|    |      |-- AppDbContext.cs
|    |-- Extensions
|    |      |-- InfrastructureExtensions.cs
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

- ✅ Clean architecture (**API**, **Application**, **Domain**, **Infrastructure**).
- ✅ Authentication & Authorization using **ASP.NET Identity**.
- ✅ Role-based Access Control (`Admin`, `Teacher`, `Student`).
- ✅ JWT Token Authentication.
- ✅ Result Design Pattern (Result<T>) for predictable business outcome handling.
- ✅ Automated HTTP Status Code Mapping using custom Result.ToActionResult() extension.
- ✅ AutoMapper for mapping Entities ↔ DTOs ↔ Domains.
- ✅ FluentValidation for input validation.
- ✅ Centralized Exception Handling Middleware.
- ✅ Rate Limiting & Request Profiling Middleware.
- ✅ Repository + Unit of Work pattern for Infrastructure.

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
   git clone
   https://github.com/HasanRaisan/CleanArchitecture_TeacherStudentAPI.git
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
