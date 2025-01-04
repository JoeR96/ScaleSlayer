using ScaleSlayer.Domain.ScaleAggregate;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.UnitTests.Helpers;

public static class ScaleHelper
{
    public static Scale CreateScale(Note rootNote, ScaleType scaleType) => Scale.CreateNew(rootNote, scaleType);
}