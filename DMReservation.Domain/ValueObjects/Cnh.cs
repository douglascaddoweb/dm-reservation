namespace DMReservation.Domain.ValueObjects
{
    public class Cnh
    {
        public string Value { get; private set; }
        public bool IsValid { get; private set; }

        protected Cnh()
        {
            
        }

        public Cnh(string value)
        {
            Value = value;
            Validate();
        }

        private void Validate()
        {
            string cnh = Value;
            IsValid = false;

            if (cnh.Length == 11 && cnh != new string('1', 11))
            {
                int dsc = 0;
                int v = 0;
                for (int i = 0, j = 9; i < 9; i++, j--)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }

                int vl1 = v % 11;
                if (vl1 >= 10)
                {
                    vl1 = 0;
                    dsc = 2;
                }

                v = 0;
                for (int i = 0, j = 1; i < 9; ++i, ++j)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }

                int x = v % 11;
                int vl2 = (x >= 10) ? 0 : x - dsc;

                IsValid = vl1.ToString() + vl2.ToString() == cnh.Substring(cnh.Length - 2, 2);

            }
        }

    }
}
