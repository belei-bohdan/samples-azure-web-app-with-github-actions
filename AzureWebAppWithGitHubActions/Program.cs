var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/test", ()
        => Results.Ok("This application has been successfully deployed to Azure Web App using GitHub Actions."))
    .WithName("Test")
    .WithOpenApi();

await app.RunAsync();