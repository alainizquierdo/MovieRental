using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

builder.Services.AddScoped<IMovieFeatures, MovieFeatures>();
//builder.Services.AddSingleton<IRentalFeatures, RentalFeatures>();
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();

/*
 Singletons are created once and shared throughout the application's lifetime.
 Scoped services are created once per request.
 the DI container does not support injecting scoped services into singletons.
 */

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var client = new MovieRentalDbContext())
{
	client.Database.EnsureCreated();
}

app.Run();
