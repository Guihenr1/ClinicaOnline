using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ClinicaOnline.Web.Attributes;
using ClinicaOnline.Web.Filters;
using ClinicaOnline.Web.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

LoggingConfig.ConfigureLogging();
builder.Host.UseSerilog();

builder.Services.AddControllers();
DependencyInjectionConfig.ConfigureAspnetRunServices(builder.Services, builder.Configuration);
Configuration.ConfigureJwt(builder.Services, builder.Configuration);
SwaggerConfig.ConfigureSwagger(builder.Services);

builder.Services.AddAuthorization(authConfig =>
{
    authConfig.AddPolicy("ApiKeyPolicy",
        policyBuilder => policyBuilder
            .AddRequirements(new ApiKeyRequirement()));
});

builder.Services.AddMvc(options => options.Filters.Add<NotificationFilter>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClinicaOnline.Web v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
           
// Configuration.SeedDatabase(app);

app.Run();
