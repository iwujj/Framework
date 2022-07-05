namespace Framework.Common
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
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
        public static string ReadConnectionString => Configuration.GetConnectionString("Read");
        public static string WriteConnectionString => Configuration.GetConnectionString("Write");

        /// <summary>
        /// JwtSettings
        /// </summary>
        public static JwtSettings JwtSettings
        {
            get
            {
                return new JwtSettings();
            }
        }
    }
}
