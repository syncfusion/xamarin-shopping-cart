using System.Web.Http;
using System.Web.Mvc;

namespace ShoppingAPI.Areas.HelpPage
{
    /// <summary>
    /// This class is responsible for help page area registration.
    /// </summary>
    public class HelpPageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the area name value.
        /// </summary>
        public override string AreaName => "HelpPage";

        /// <summary>
        /// Register the area.
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new {controller = "Help", action = "Index", apiId = UrlParameter.Optional});

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}