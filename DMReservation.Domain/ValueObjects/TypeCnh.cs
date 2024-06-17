namespace DMReservation.Domain.ValueObjects
{
    public class TypeCnh
    {
        public string Value { get; private set; }
        public bool IsValid { get; private set; }

        protected TypeCnh()
        {
            
        }

        public TypeCnh(string value)
        {
            switch (value)
            {
                case "A":
                case "B":
                case "AB":
                    IsValid = true;
                    Value = value; 
                    break;
                default:
                    Value = value;
                    IsValid = false; 
                    break;
            }
        }
    }
}
