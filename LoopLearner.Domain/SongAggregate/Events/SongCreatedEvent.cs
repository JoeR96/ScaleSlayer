using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.SongAggregate.Events;

public record SongCreatedEvent(Song Song) : IDomainEvent;