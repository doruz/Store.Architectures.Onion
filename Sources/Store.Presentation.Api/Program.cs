using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CosmosExceptionsFilter>();
    options.Filters.Add<BusinessExceptionsFilter>();
});
builder.Services.AddOpenApi();

builder.Services.AddCurrentSolution(builder.Configuration);

builder.Services
    .Configure<ApiBehaviorOptions>(options => options.SuppressMapClientErrors = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Store API";
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
