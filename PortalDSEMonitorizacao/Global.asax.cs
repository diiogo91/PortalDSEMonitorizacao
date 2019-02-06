using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ELFinder.Connector.ASPNet.ModelBinders;
using ELFinder.Connector.Config;
using System.Diagnostics;
using PortalDSEMonitorizacao.Config;
using PortalDSEMonitorizacao.Models.Libraries;
using MongoDB.Bson;
using System.Web.Http;
using System;

namespace PortalDSEMonitorizacao
{
    public class Global : HttpApplication
    {
        #region Methods

        /// <summary>
        /// Application start
        /// </summary>
        protected void Application_Start()
        {

            // Standard registrations
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            //Fix ObjectId null value returned from Model to Controller
            ModelBinders.Binders.Add(typeof(ObjectId), new ObjectIdModelBinder());
            // Use custom model binder
            ModelBinders.Binders.DefaultBinder = new ELFinderModelBinder();

            // Initialize ELFinder configuration
            InitELFinderConfiguration();

            //JSON API SUPPORT
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
            .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
                                          "text/html",
                                          StringComparison.InvariantCultureIgnoreCase,
                                          true,
                                          "application/json"));

        }

        /// <summary>
        /// Initialize ELFinder configuration
        /// </summary>
        protected void InitELFinderConfiguration()
        {

            SharedConfig.ELFinder = new ELFinderConfig(
               // Server.UrlPathEncode(@"D:\PortalMon"),
                Server.UrlPathEncode(@"\\semzseiptg12\PortalMon"),
                thumbnailsUrl: "Thumbnails/"
                );

            Debug.WriteLine(Server.UrlPathEncode(@"\\semzseiptg12\PortalMon"));

            SharedConfig.ELFinder.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    Server.UrlPathEncode(@"\\semzseiptg12\PortalMon\DOCUMENTACAO"),
                    isLocked: false,
                    isReadOnly: false,
                    isShowOnly: false,
                    maxUploadSizeKb: null,      // null = Unlimited upload size
                    uploadOverwrite: true,
                    startDirectory: ""));

        }

        #endregion
    }
}