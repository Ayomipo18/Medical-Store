using Hangfire;
using MedicalStore.Extensions;
using MedicalStore.Middlewares;
using MedicalStore.Presentation.Helpers;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Repository.DbContext;
using Shared.Helpers;

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
    new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
    .Services.BuildServiceProvider()
    .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
    .OfType<NewtonsoftJsonPatchInputFormatter>().First();

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfifureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureAzureServices();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureApplicationService();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureSqlContextandHangFire(builder.Configuration);
builder.Services.ConfigureMvc();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddXmlDataContractSerializerFormatters()
.AddApplicationPart(typeof(MedicalStore.Presentation.AssemblyReference).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.SeedRoleData().Wait();
app.SeedUserData().Wait();

// Configure the HTTP request pipeline.

if (app.Environment.IsProduction())
{
    app.UseHsts();
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[] { new HangFireAuthorizationFilter(builder.Configuration) }
    });
} else if(app.Environment.IsDevelopment())
{
    app.UseHangfireDashboard();
}

app.UseSwagger();
app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/api/swagger.json", "Medical Store API"));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseErrorHandler();

app.MapControllers();

WebHelper.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

app.Run();
