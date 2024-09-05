using LoopLearner.Application.Contracts.Services;

namespace LoopLearner.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
