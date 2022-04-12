namespace WebApi.Extensions;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class RangeValidationAttribute : ValidationAttribute
{
    public int Min { get; set; }
    public int Max { get; set; }

    public RangeValidationAttribute()
    {
        Min = 0;
        Max = int.MaxValue;
    }

    public override bool IsValid(object value)
    {
        if (value is IEnumerable<int> list && list.Any())
        {
            return !list.Any(i => i < Min || i > Max);
        }

        return true;
    }
}
