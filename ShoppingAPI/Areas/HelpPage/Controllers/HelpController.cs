using System.Web.Http;
using System.Web.Mvc;
using ShoppingAPI.Areas.HelpPage.ModelDescriptions;

namespace ShoppingAPI.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// Error name field.
        /// </summary>
        private const string ErrorViewName = "Error";

        /// <summary>
        /// Initializes the help controller.
        /// </summary>
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        /// <summary>
        /// Initializes the help controller with http configuration.
        /// </summary>
        /// <param name="config"></param>
        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        /// <summary>
        /// Get the http configuration.
        /// </summary>
        public HttpConfiguration Configuration { get; }

        /// <summary>
        /// Get the action results.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        /// <summary>
        /// Get the action result based on api ID.
        /// </summary>
        /// <param name="apiId"></param>
        /// <returns></returns>
        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                var apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null) return View(apiModel);
            }

            return View(ErrorViewName);
        }

        /// <summary>
        /// Get the action result based on model name.
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                var modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                    return View(modelDescription);
            }

            return View(ErrorViewName);
        }
    }
}