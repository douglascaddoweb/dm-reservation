using System.ComponentModel;

namespace DMReservation.Domain.Enums
{
    public enum StatusOrder : short
    {
        [Description("Available")]
        Available = 1,
        
        [Description("Accept")]
        Accept = 2,
        
        [Description("Accept")]
        Delivered = 3,

    }
}
