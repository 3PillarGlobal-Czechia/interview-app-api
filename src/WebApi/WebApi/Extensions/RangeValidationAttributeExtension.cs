namespace WebApi.Extensions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class RangeValidationAttributeExtension : ValidationAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public RangeValidationAttributeExtension()
        {
            Min = 0;
            Max = int.MaxValue;
        }

        public override bool IsValid(object value)
        {
            if (value is IEnumerable<int> list && list.Count() > 0)
            {
                if (list.Any(i => i < Min || i > Max))
                {
                    return false;
                }
                return true;
            }

            return true;
        }
    }
}
