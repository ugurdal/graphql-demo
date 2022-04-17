using GraphQL.API.Schema.Queries;
using GraphQL.API.Schema.Mutations;
using GraphQL.API.Schema.Subscriptions;
using GraphQL.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using GraphQL.API.Services;
using GraphQL.API.DataLoaders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddFiltering()
    .AddSorting();

builder.Services.AddInMemorySubscriptions();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddPooledDbContextFactory<AppDbContext>(
    o => o.UseSqlite(connectionString: connectionString));

// repository
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<InstructorRepository>();

// dataloader
builder.Services.AddScoped<InstructorDataLoader>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Run migrations
using (var scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    await using var context = contextFactory.CreateDbContext();
    context.Database.Migrate();
    await context.SeedAsync();
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseWebSockets(); // GraphQL subscriptions require web socket

app.UseAuthorization();
app.MapControllers();
app.MapGraphQL();

app.Run();