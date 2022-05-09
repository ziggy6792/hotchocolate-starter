using ConferencePlanner.GraphQL;
using ConferencePlanner.GraphQL.Attendees;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using ConferencePlanner.GraphQL.Sessions;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));
// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));
builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
        .AddTypeExtension<SpeakerQueries>()
        .AddTypeExtension<SessionQueries>()
        .AddTypeExtension<TrackQueries>()
    .AddMutationType<Mutation>()
       .AddTypeExtension<AttendeeMutations>()
        .AddTypeExtension<SpeakerMutations>()
        .AddTypeExtension<SessionMutations>()
        .AddTypeExtension<TrackMutations>()
         .AddType<AttendeeType>()
    .AddSubscriptionType<Subscription>()
     .AddTypeExtension<AttendeeSubscriptions>()
     .AddTypeExtension<SessionSubscriptions>()
    .AddType<SessionType>()
    .AddType<TrackType>()
    .AddType<SpeakerType>()
     .AddGlobalObjectIdentification()
     .AddQueryFieldToMutationPayloads()
    .AddDataLoader<SpeakerByIdDataLoader>()
    .AddDataLoader<SessionByIdDataLoader>()
    .AddFiltering()
   .AddSorting()
   .AddInMemorySubscriptions();


var app = builder.Build();

app.UseWebSockets();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
  endpoints.MapGraphQL();
});

app.Run();