using AutoMapper;
using CSV_Reader.Data;
using CSV_Reader.Implementations.Converters;
using CSV_Reader.Implementations.FileExporters;
using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Interfaces.Exporters;
using CSV_Reader.Mappings;
using CSV_Reader.Models;
using CSV_Reader.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ApplicationDbContext>();

builder.Services.AddScoped<ICsvValidator, CsvValidator>();
builder.Services.AddScoped<ICsvRepository, CsvRepository>();
builder.Services.AddScoped<ICsvUtility, CsvUtility>();

//builder.Services.AddScoped<ICsvConverter, CsvToXmlConverter>();

builder.Services.AddScoped<ICsvToXmlConverter, CsvToXmlConverter>();
builder.Services.AddScoped<ICsvToHtmlConverter, CsvToHtmlConverter>();
builder.Services.AddScoped<ICsvToJsonConverter, CsvToJsonConverter>();
builder.Services.AddScoped<ICsvToYamlConverter, CsvToYamlConverter>();
builder.Services.AddScoped<ICsvToDotNetObjectConverter, CsvToDotNetObjectConverter>();

builder.Services.AddScoped<IJsonExporter, JsonExporter>();
builder.Services.AddScoped<IXmlExporter, XmlExporter>();
builder.Services.AddScoped<IYamlExporter, YamlExporter>();
builder.Services.AddScoped<IHtmlExporter, HtmlExporter>();
builder.Services.AddScoped<ICsvExporter, CsvExporter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
