using ConferencePlanner.GraphQL.Data;
using HotChocolate.Types.Relay;

namespace ConferencePlanner.GraphQL.Attendees
{
  public record CheckInAttendeeInput(
      [property: ID]
        int SessionId,
      [property: ID]
        int AttendeeId);
}