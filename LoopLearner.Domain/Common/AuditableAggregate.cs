using LoopLearner.Domain.Common.Interfaces;
using LoopLearner.Domain.UserAggregate;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Domain.Common;

public abstract class AuditableAggregate<TId> : AggregateRoot<TId>, IAuditable where TId : notnull
{
    public User? CreatedBy { get; set; }
    public UserId? CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public User? LastModifiedBy { get; set; }
    public UserId? LastModifiedByUserId { get; set; }
    public DateTime? LastModifiedOn { get; set; }

}