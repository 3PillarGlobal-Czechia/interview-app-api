using System;

namespace Domain.Entities;

public abstract class EntityBase
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
