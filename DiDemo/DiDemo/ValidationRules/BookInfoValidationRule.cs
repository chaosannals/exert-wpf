using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DiDemo.ValidationRules;

public class BookInfoValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value == null)
        {
            return new ValidationResult(false, "NOT NULL");
        }

        if (!value.GetType().IsInstanceOfType(typeof(decimal)))
        {
            return new ValidationResult(false, "MUST DECIMAL");
        }

        var p = (decimal)value;

        if (p <= 0)
        {
            return new ValidationResult(false, "MUST GREET ZERO");
        }

        //return new ValidationResult(false, "MUST GREET ZERO");
        return ValidationResult.ValidResult;
    }
}
