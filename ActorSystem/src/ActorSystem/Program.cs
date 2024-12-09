using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ActorSystem;
using System.Xml;
using ActorSystem.Communication;

var actorSystem = new ActorSystemMVP();
string pathToXML = "./ProccesExample/LoadHomeWorks.xml";
var document = new XmlDocument();
document.Load(pathToXML);
actorSystem.loadXml(document);


actorSystem.AddRedirectRule(new RedirectRule("WebForm","Send","WriteInDB"));
actorSystem.Start();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(actorSystem);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionURI);
});
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();