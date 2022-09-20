namespace SchedulingApplication
{
    public class AppSettings
    {
        public SmtpSettings? SmtpSettings { get; set; }

    }

    public class SmtpSettings
    {
        public string? Server { get; set; }
        public string? Email { get; set; }
        public int? Port { get; set; }
        public string? Password { get; set; }
        public string? EmailAddressOrders { get; set; }
        public string? SmtpPasswordOrders { get; set; }
    }

}
