using System;

namespace Domain.Models;

public abstract class ModelBase
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
