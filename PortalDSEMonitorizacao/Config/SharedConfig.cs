using ELFinder.Connector.Config;
using MongoDB.Bson;
using System.Web.Mvc;

namespace PortalDSEMonitorizacao.Config
{

    /// <summary>
    /// Shared config
    /// </summary>
    public static class SharedConfig
    {

        #region Properties

        /// <summary>
        /// ELFinder shared configuration
        /// </summary>
        public static ELFinderConfig ELFinder { get; set; }

        #endregion
    }

}