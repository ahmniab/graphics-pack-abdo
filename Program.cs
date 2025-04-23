using Graphics.Models;
using Graphics.Graphics;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<TransformationsState>();
builder.Services.AddSingleton<TransformationsFactory>();
var app = builder.Build();


app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/")
    .WithStaticAssets();
string GeneratedImagesPath =  Path.Combine(app.Environment.ContentRootPath, "wwwroot", "assets", "Generated");
if (!Directory.Exists(GeneratedImagesPath))
{
    Directory.CreateDirectory(GeneratedImagesPath); // Ensure the directory exists
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(GeneratedImagesPath),
    RequestPath = "/Generated"
});
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/Draw/{*catchall}", "/Draw/Main");
app.MapFallbackToPage("/Transformations/{*catchall}", "/Transformations/Main");
app.Run();
