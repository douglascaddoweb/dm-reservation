namespace DMReservation.Domain.Settings
{
    public static class MessageSetting
    {
        public static string MotorcycleRegistered = "License Plate already registry.";
        public static string CnpjInvalid = "The CNPJ is invalid.";
        public static string CnpjExist = "The CNPJ already registered.";
        public static string CnhExist = "The CNH already registered.";
        public static string CnhInvalid = "The CNH is invalid.";
        public static string TypeCnhInvalid = "The Type CNH is invalid.";
        public static string RegistryNotFound = "Registry does not found.";
        public static string FormatImageInvalid = "Format image invalid.";
        public static string UploadImageError = "There was an error uploading file.";
        public static string DateFinishInvalid = "Date finish should be more than today.";
        public static string RemoveMotorcycle = "It is not possible to delete the motorcycle";
        public static string ProcessError = "Was not possible process your request.";
        public static string PermissionDelivery = "Delivery not permitted";
        public static string AcceptNotPossible = "It is not possible to accept this delivery";
        public static string ErrorGenereateValueRental = "Impossible generate the value to the rental.";
        public static string AnythingMotorcyleToRent = "Don't have any motorcycle to rent.";
        public static string DeliveryManNotFound = "Not found any delivery man";
        public static string DeliveryManNotHabilit = "Delivery man can't drive motorcycle.";
        public static string LincensePlateInvalid = "License Plate invalid.";
    }
}
