using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using ProjectPractice.Database;
using ProjectPractice.Services;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try {
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    // IgnoreCycles из-за AutoInclude навигаций, иначе рекурсивная сериализация
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddDbContext<StudentDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddStudentServices();

    WebApplication app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthorization();

    app.MapControllers();
    
    logger.Info("Hello World!");

    app.Run();
}
catch (Exception ex) {
    logger.Error(ex, "Stopped program because of exception");
}
finally {
    LogManager.Shutdown();
}