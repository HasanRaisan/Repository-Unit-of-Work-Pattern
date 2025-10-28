using Clean_Three_Tier_First.Midlleware;
using Data.Data;
using Data.Identity;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyConnection"),
        b => b.MigrationsAssembly("Data"))
);


//builder.Services.AddControllers()
//    .AddFluentValidation(options =>
//    {
//        options.DisableDataAnnotationsValidation = true;

//        options.RegisterValidatorsFromAssemblyContaining<IAuthService>();
//    });


builder.Services.AddIdentityConfiguration();




var app = builder.Build();



// Custum Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>(); // 1. Exception handling (catches all unhandled errors)
app.UseMiddleware<AdvancedProfilingMiddleware>(); // 2. Rate limiting (can be placed before Profiling if you want to block excessive requests first)
app.UseMiddleware<RateLimitingMiddleware>();      // 3. Profiling (measures the time for all subsequent requests)






// Seed Roles and Adimn
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    await ApplicationDbContextSeed.SeedRolesAndAdminAsync(service);
}

app.UseSwagger();
app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*

 BusinessLayer
|
|-- Configuration  // جديد: لإعدادات التطبيق (مثل JWT)
|    |-- JwtSettings.cs  // تغيير الاسم ليعكس مجموعة الإعدادات (صيغة الجمع)
|
|-- Constants      // جديد: للثوابت العامة
|    |-- RoleConstants.cs  // تم نقل الملف إلى هنا (اختياري)
|
|-- Mapping           // لنقل البيانات بين الطبقات (Entities/Domain)
|    |-- // ملفات DTOs هنا (مثل UserDto.cs, AppointmentDto.cs)
|
|-- Models         // نماذج الطلب/الاستجابة المستخدمة في الخدمات
|    |-- Auth
|    |    |-- AssignRoleModel.cs
|    |    |-- AuthResultModel.cs  // تغيير الاسم من AuthModel.cs ليكون أكثر تحديداً
|    |    |-- LoginModel.cs
|    |    |-- RegisterModel.cs    // تصحيح إملائي من ReqisterModel.cs
|    |
|    |-- AppointmentModel.cs
|    |-- PatientModel.cs
|
|-- Services       // منطق العمل وتطبيق الواجهات
|    |-- Auth
|    |    |-- AuthService.cs
|    |    |-- IAuthService.cs
|    |    |-- ITokenService.cs
|    |    |-- TokenService.cs
|    |
|    |-- Appointments
|    |    |-- AppointmentService.cs  // نقل إلى مجلد فرعي
|    |    |-- IAppointmentService.cs // إضافة الواجهة
|    |
|    |-- Patients
|    |    |-- PatientService.cs    // نقل إلى مجلد فرعي
|    |    |-- IPatientService.cs     // إضافة الواجهة
|    |
|    |-- ServiceCollectionExtensions.cs // لتسجيل الخدمات في حاوية IoC

 */


// note : hard code (roles in data) 
// services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



