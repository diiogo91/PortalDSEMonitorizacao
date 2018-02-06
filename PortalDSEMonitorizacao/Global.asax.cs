using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ELFinder.Connector.ASPNet.ModelBinders;
using ELFinder.Connector.Config;
using System.Diagnostics;
using PortalDSEMonitorizacao.Config;

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

            // Use custom model binder
            ModelBinders.Binders.DefaultBinder = new ELFinderModelBinder();

            // Initialize ELFinder configuration
            InitELFinderConfiguration();

        }

        /// <summary>
        /// Initialize ELFinder configuration
        /// </summary>
        protected void InitELFinderConfiguration()
        {

            SharedConfig.ELFinder = new ELFinderConfig(
                Server.UrlPathEncode(@"D:\PortalMon"),
                thumbnailsUrl: "Thumbnails/"
                );

            Debug.WriteLine(Server.UrlPathEncode(@"D:\PortalMon"));

            SharedConfig.ELFinder.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    Server.UrlPathEncode(@"D:\PortalMon\DOCUMENTACAO"),
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