using System.ComponentModel.DataAnnotations;

namespace NotefyMe.Domain.Validations
{
    public class MaximumAgeAttribute : ValidationAttribute
    {
        private readonly int _maximumAge;

        public MaximumAgeAttribute(int maximumAge)
        {
            _maximumAge = maximumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;

            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_maximumAge) > DateTime.Now;
            }

            return false;
        }
    }
}
