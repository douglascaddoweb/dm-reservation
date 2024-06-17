using System.ComponentModel;

namespace DMReservation.Domain.Enums
{
    public enum StatusRental : short
    {
        [Description("Leased")]
        Leased = 1,
        [Description("Delivered")]
        Delivered = 2
    }
}
