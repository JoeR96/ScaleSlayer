using LoopLearner.Domain.ScaleAggregate;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.UnitTests.Helpers;

public static class ScaleHelper
{
    public static Scale CreateScale(Note rootNote, ScaleType scaleType) => Scale.CreateNew(rootNote, scaleType);
}