# Clean Architecture Web API (.NET 9)

A professional, scalable, and modular **Clean Architecture** implementation using **.NET 9 Web API**. This project follows industry best practices and clean separation of concerns across four main layers:

- **API Layer** – Entry point, controllers, middleware, configurations.
- **Application Layer** – Business logic, DTOs, validators, services, mappings.
- **Domain Layer** – Core domain entities, rules, and business constraints.
- **Infrastructure Layer** – EF Core, repositories, database context, migrations, persistence.

This structure promotes **testability**, **maintainability**, **scalability**, and clean separation of responsibilities.

---

# 📁 Folder Structure

## **API Layer**

```
API
 |-- Properties
 |      |-- launchSettings.json
 |
 |-- Controllers
 |      |-- DepartmentController.cs
 |      |-- StudentController.cs
 |      |-- TeacherController.cs
 |      |-- UserController.cs
 |
 |-- Midlleware
 |      |-- AdvancedProfilingMiddleware.cs
 |      |-- ExceptionHandlingMiddleware.cs
 |      |-- RateLimitingMiddleware.cs
 |
 |-- packages.cs
 |-- appsettings.json
 |-- Program.cs
 |-- TeacherStudentAPI.http
```

---

## **Application Layer**

```
Application
 |-- Configruration
 |      | -- JwtSettings.cs
 |-- Constants
 |      | -- RoleConstants.cs
 |-- DTOs
 |      | -- Department
 |      |       |-- DepartmentBaseDTO.cs
 |      |       |-- DepartmentCreateDTO.cs
 |      |       |-- DepartmentDTO.cs
 |      |       |-- DepartmentUpdateDTO.cs
 |      | -- Identity
 |      |       |-- AssignRoleDTO.cs
 |      |       |-- AuthResultDTO.cs
 |      |       |-- LoginDTO.cs
 |      |       |-- RegisterDTO.cs
 |      | -- Student
 |      |       |-- StudentBaseDTO.cs
 |      |       |-- StudentCreateDTO.cs
 |      |       |-- StudentDTO.cs
 |      |       |-- StudentUpdateDTO.cs
 |      | -- Teacher
 |      |       |-- TeacherBaseDTO.cs
 |      |       |-- TeacherCreateDTO.cs
 |      |       |-- TeacherDTO.cs
 |      |       |-- TeacherUpdateDTO.cs
 |
 |-- Extensions
 |      |-- ServiceCollectionExtensions.cs
 |
 |-- Mapping
 |      |-- MappingProfile.cs
 |
 |-- Result
 |      |-- Error.cs
 |      |-- ErrorType.cs
 |      |-- Result.cs
 |      |-- ResultFactory.cs
 |      |-- ResultToActionMapper.cs
 |
 |-- Services
 |      |-- Auth
 |      |     |-- IAuthService.cs
 |      |     |-- AuthService.cs
 |      |     |-- ITokenService.cs
 |      |     |-- TokenService.cs
 |      |-- Department
 |      |     |-- DepartmentService.cs
 |      |     |-- IDepartmentService.cs
 |      |-- Generic
 |      |     |-- IGenericService.cs
 |      |-- Logging
 |      |     |-- ErrorLogService.cs
 |      |     |-- IErrorLogService.cs
 |      |-- Student
 |      |     |-- IStudentService.cs
 |      |     |-- StudentService.cs
 |      |-- Teacher
 |      |     |-- ITeacherService.cs
 |      |     |-- TeacherService.cs
 |
 |-- Validation
 |      |-- Department
 |      |     |-- DepartmentBaseValidator.cs
 |      |     |-- DepartmentCreateDTOValidator.cs
 |      |     |-- DepartmentUpdateDTOValidator.cs
 |      |-- Identity
 |      |     |-- AssignRoleDTOValidator.cs
 |      |     |-- LoginDTOValidator.cs
 |      |     |-- RegisterDTOValidator.cs
 |      |-- Student
 |      |     |-- StudentBaseValidator.cs
 |      |     |-- StudentCreateDTOValidator.cs
 |      |     |-- StudentUpdateDTOValidator.cs
 |      |-- Teacher
 |      |     |-- TeacherBaseValidator.cs
 |      |     |-- TeacherCreateDTOValidator.cs
 |      |     |-- TeacherUpdateDTOValidator.cs
```

---

## **Domain Layer**

```
Domain
 |-- Entites
 |      |-- Auth
 |      |     |-- AssignRole
 |      |     |     |-- AssignRoleDomain.cs
 |      |     |     |-- AssignRoleRules.cs
 |      |     |-- Login
 |      |     |     |-- LoginDomain.cs
 |      |     |     |-- LoginRules.cs
 |      |     |-- Register
 |      |     |     |-- RegisterDomain.cs
 |      |     |     |-- RegisterRules.cs
 |      |
 |      |-- Department
 |      |     |-- DepartmentDomain.cs
 |      |     |-- DepartmentRules.cs
 |
 |      |-- Studnet
 |      |     |-- StudnetDomain.cs
 |      |     |-- StudnetRules.cs
 |
 |      |-- Teacher
 |      |     |-- TeacherDomain.cs
 |      |     |-- TeacherRules.cs
 |
 |-- Results
 |      |-- Result
```

---

## **Infrastructure Layer**

```
Infrastructure
 |-- Data
 |      |-- Entities
 |      |      |-- ApplicationRoleEntity.cs
 |      |      |-- ApplicationUserEntity.cs
 |      |      |-- DepartmentEntity.cs
 |      |      |-- ErrorLogEntity.cs
 |      |      |-- StudentEntity.cs
 |      |      |-- TeacherEntity.cs
 |      |
 |      |-- AppDbContext.cs
 |
 |-- Extensions
 |      |-- InfrastructureExtensions.cs
 |
 |-- Identity
 |      |-- ApplicationDbContextSeed.cs
 |
 |-- Migrations
 |
 |-- Repositories
 |      |-- Generic
 |      |     |-- IRepository.cs
 |      |     |-- Repository.cs
 |      |-- ErrorLog
 |      |     |-- IErrorLogRepository.cs
 |      |     |-- ErrorLogRepository.cs
 |      |-- Teacher
 |      |     |-- ITeacherRepository.cs
 |      |     |-- TeacherRepository.cs
 |
 |-- UnitOfWork
 |      |-- IUnitOfWork.cs
 |      |-- UnitOfWork.cs
```

---

# 🚀 Features

- ✅ Clean architecture (**API**, **Application**, **Domain**, **Infrastructure**).
- ✅ Authentication & Authorization using **ASP.NET Identity**.
- ✅ Role-based Access Control (`Admin`, `Teacher`, `Student`).
- ✅ JWT Token Authentication.
- ✅ Result Design Pattern (Result<T>) for predictable business outcome handling.
- ✅ Automated HTTP Status Code Mapping using custom Result.ToActionResult() extension.
- ✅ AutoMapper for mapping Entities ↔ DTOs
- ✅ FluentValidation for input validation.
- ✅ Centralized Exception Handling Middleware.
- ✅ Rate Limiting & Request Profiling Middleware.
- ✅ Repository + Unit of Work pattern for Infrastructure.

---

# 🛠️ Technologies

- **.NET 9 (ASP.NET Core Web API)**
- **Entity Framework Core**
- **FluentValidation**
- **AutoMapper**
- **Identity with Roles**
- **SQL Server**
- **Swagger (API Documentation)**

---

# 🧩 How to Run

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

# 👨‍💻 Author

**Hasan Raisan**  
📧 hasan.raisann@gmail.com  
💼 Web Developer
