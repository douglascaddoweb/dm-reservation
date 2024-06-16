using System.ComponentModel.DataAnnotations;

namespace DMReservation.API.Model.Attibutes
{
    public sealed class ValidateDateAttribute : ValidationAttribute
    {


        public override string FormatErrorMessage(string name)
        {
            return $"The {name} is should between 1900 and {DateTime.Now.AddYears(1).Year}";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;


            short year = (short)DateTime.Now.AddYears(1).Year;

            if ((short)value < 1900 || (short)value > year) return false;

            return true;
        }

    }
}
