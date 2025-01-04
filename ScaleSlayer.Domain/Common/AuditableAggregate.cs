using ScaleSlayer.Domain.Common.Interfaces;
using ScaleSlayer.Domain.UserAggregate;
using ScaleSlayer.Domain.UserAggregate.ValueObjects;

namespace ScaleSlayer.Domain.Common;

public abstract class AuditableAggregate<TId> : AggregateRoot<TId>, IAuditable where TId : notnull
{
    public User? CreatedBy { get; set; }
    public UserId? CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public User? LastModifiedBy { get; set; }
    public UserId? LastModifiedByUserId { get; set; }
    public DateTime? LastModifiedOn { get; set; }

}