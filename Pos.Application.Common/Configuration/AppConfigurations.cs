namespace Pos.Application.Common.Configuration
{
    public class AppConfigurations
    {
        static AppConfigurations()
        {
            Configuration = new AppConfigurations();
        }
        public static AppConfigurations Configuration { get; set; }
    }
}
