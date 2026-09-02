using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using ProjectPractice.Database;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try {
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    builder.Services.AddControllers();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddDbContext<StudentDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    WebApplication app = builder.Build();

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