using LoopLearner.Domain.UserAggregate;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Domain.Common.Interfaces;

public interface IAuditable
{
    User? CreatedBy { get; set; }
    UserId? CreatedByUserId { get; set; }
    DateTime CreatedOn { get; set; }
    User? LastModifiedBy { get; set; }
    UserId? LastModifiedByUserId { get; set; }
    DateTime? LastModifiedOn { get; set; }
}