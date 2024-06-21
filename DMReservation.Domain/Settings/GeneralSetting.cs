namespace DMReservation.Domain.Settings
{
    public static class GeneralSetting
    {
        public static string ConnectionString { get; set; }

        public static string HostRabbit { get; set; }
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static int Port { get; set; }

        public static string ChannelConsumer { get; set; }
    }
}
