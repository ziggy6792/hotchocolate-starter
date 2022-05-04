using ConferencePlanner.GraphQL;
using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();


// Configure the HTTP request pipeline.


// app.UseHttpsRedirection();


// app.MapControllers();
app.UseWebSockets();

app.UseRouting();
// app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  endpoints.MapGraphQL();
});

app.Run();