using Microsoft.EntityFrameworkCore;
using ReminderChallenge.Repository;
using ReminderChallenge.Repository.Repositories;
using ReminderChallenge.Service.Configuration;
using ReminderChallenge.Service.Helpers;
using ReminderChallenge.Service.Services.EmailService;
using ReminderChallenge.Service.Services.NotificationService;
using ReminderChallenge.Service.Services.PushNotificationService;
using ReminderChallenge.Service.Services.RemindersService;

var builder = WebApplication.CreateBuilder(args);

CheckFireBaseConnections(builder);

builder.Services.Configure<MailSettings>(
    builder.Configuration.GetSection("MailSettings"));

builder.Services.Configure<ReminderNotificationSettings>(
    builder.Configuration.GetSection("ReminderNotificationSettings"));

builder.Services.Configure<PushNotificationSettings>(
    builder.Configuration.GetSection("PushNotificationSettings"));
// Add services to the container.
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
builder.Services.AddScoped<IReminderService, ReminderService>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddHostedService<NotificationService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReminderContext>(options =>
    options.UseSqlite("Data Source=Reminder_Challenge.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void CheckFireBaseConnections(WebApplicationBuilder builder)
{
    builder.Logging.AddConsole();
    var logger = LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger("FirebaseInit");

    FirebaseManager.Initialize("Resources/firebase-credentials.json", logger);
}