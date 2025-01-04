using ScaleSlayer.Application.Contracts.Services;

namespace ScaleSlayer.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
