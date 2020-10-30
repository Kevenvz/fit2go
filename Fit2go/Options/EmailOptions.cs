namespace Fit2go.Options
{
    public class EmailOptions
    {
        public const string Section = "Email";

        public string SendgridApiKey { get; set; }
        public string EmailFrom { get; set; }
        public string NameFrom { get; set; }
    }
}
