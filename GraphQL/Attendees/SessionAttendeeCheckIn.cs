using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types.Relay;

namespace ConferencePlanner.GraphQL.Attendees
{
  public class SessionAttendeeCheckIn
  {
    public SessionAttendeeCheckIn(int attendeeId, int sessionId)
    {
      AttendeeId = attendeeId;
      SessionId = sessionId;
    }

    [property: ID]
    public int AttendeeId { get; }

    [property: ID]
    public int SessionId { get; }

    [UseApplicationDbContext]
    public async Task<int> CheckInCountAsync(
        [ScopedService] ApplicationDbContext context,
        CancellationToken cancellationToken) =>
        await context.Sessions
            .Where(session => session.Id == SessionId)
            .SelectMany(session => session.SessionAttendees)
            .CountAsync(cancellationToken);

    public Task<Attendee> GetAttendeeAsync(
        AttendeeByIdDataLoader attendeeById,
        CancellationToken cancellationToken) =>
        attendeeById.LoadAsync(AttendeeId, cancellationToken);

    public Task<Session> GetSessionAsync(
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken) =>
        sessionById.LoadAsync(AttendeeId, cancellationToken);
  }
}