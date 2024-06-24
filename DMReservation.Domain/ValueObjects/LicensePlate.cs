using System.Text.RegularExpressions;

namespace DMReservation.Domain.ValueObjects
{
    public class LicensePlate
    {
        public string Value { get; set; }
        public bool IsValid { get; set; }

        protected LicensePlate()
        { }

        public LicensePlate(string value)
        {
            Value = value;
            IsValid = false;
            Validate();
        }

        private void Validate()
        {
            string pattern = "^[a-zA-Z]{3}[-]{1}[0-9]{4}|[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}$";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(Value))
                IsValid = true;
            else 
                IsValid = false;
        }
    }
}
