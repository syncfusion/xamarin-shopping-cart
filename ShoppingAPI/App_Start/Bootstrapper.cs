using System.Web.Http;
using ShoppingAPI.Mappings;

namespace ShoppingAPI.App_Start
{
    /// <summary>
    /// The Class bootstrapper which prepares/configures a group of classes/objects or an entire API for your specific
    /// needs and use.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Initializes the web api configuration.
        /// </summary>
        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            MapperConfig.Configure();
        }
    }
}