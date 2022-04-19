namespace Framework.Common
{
    public  class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static string app(params string[] sections)
        {
            try
            {

                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception) { }

            return "";
        }
    }
}
