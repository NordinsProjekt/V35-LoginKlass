
using GlobalChat;
using Interfaces;
using LoginClass;
using Blazor_UI.Data;
using Buffert;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<Chat,Chat>();
builder.Services.AddSingleton<GameRoom>();
builder.Services.AddScoped<ILogin, Login>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
//Spelet med SignalR
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<GameHub>(GameHub.HubUrl);

app.Run();
