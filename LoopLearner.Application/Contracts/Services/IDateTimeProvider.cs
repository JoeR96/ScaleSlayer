namespace LoopLearner.Application.Contracts.Services;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}