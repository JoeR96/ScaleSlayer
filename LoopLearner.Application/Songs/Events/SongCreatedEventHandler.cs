using LoopLearner.Domain.SongAggregate.Events;

namespace LoopLearner.Application.Songs.Events;

public class SongCreatedEventHandler : INotificationHandler<SongCreatedEvent>
{
    public Task Handle(SongCreatedEvent notification, CancellationToken cancellationToken)
    {
        // do stuff
        Console.WriteLine($"Song Created Event Recieved for Song {notification.Song.Id} - {notification.Song.Title}");
        return Task.CompletedTask;
    }
}