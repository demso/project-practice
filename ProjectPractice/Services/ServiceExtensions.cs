namespace ProjectPractice.Services;

public static class ServiceExtensions
{
    // все сервисы бд регистрируем тут
    public static IServiceCollection AddStudentServices(this IServiceCollection services)
    {
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IDisciplineService, DisciplineService>();
        services.AddScoped<IGradeService, GradeService>();

        return services;
    }
}
