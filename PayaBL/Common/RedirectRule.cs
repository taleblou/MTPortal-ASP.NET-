namespace PayaBL.Common
{
    internal class RedirectRule
    {
        // Methods
        public RedirectRule()
        {
            this.Name = "";
            this.Rewrite = "";
            this.Url = "";
        }

        // Properties
        public string Name { get; set; }

        public string Rewrite { get; set; }

        public string Url { get; set; }
    }

}
