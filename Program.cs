using Microsoft.EntityFrameworkCore;
using rest_api_test;
using FluentValidation.AspNetCore;
using rest_api_test.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
  .AddControllers(opt => opt.Filters.Add(typeof(DBSaveChangesFilter)))
  .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<NewItemRequestValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ItemContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ItemContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "test",
                      policy  =>
                      {
                          policy
                            .WithOrigins("http://localhost:3000")
                            .WithMethods("GET", "PUT", "DELETE", "POST")
                            .WithHeaders("Content-Type");
                      });
});
builder.Services.AddAutoMapper((serviceProvider, config) => {
}, typeof(ItemContext));

var app = builder.Build();

app.UseCors("test");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
