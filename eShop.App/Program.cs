using Application.Behaviours;
using eShop.App.Middlewares;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Scan(
	selector => selector
		.FromAssemblies(
			Infrastructure.AssemblyReference.Assembly,
			Persistence.AssemblyReference.Assembly)
		.AddClasses(false)
		.AsImplementedInterfaces()
		.WithScopedLifetime());

builder.Services.AddMediatR(config =>
	config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));

builder.Services.AddControllers()
	.AddApplicationPart(Presentation.AssemblyReference.Assembly);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();
