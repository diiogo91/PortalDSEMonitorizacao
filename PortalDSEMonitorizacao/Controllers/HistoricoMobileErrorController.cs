using MongoDB.Bson;
using MongoDB.Driver;
using PortalDSEMonitorizacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PortalDSEMonitorizacao.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class HistoricoMobileErrorController : ApiController
    {
        private static PortalDSEMonDB Context = new PortalDSEMonDB();

        // GET: api/HistoricoMobile
       
        public IEnumerable<HistoricoMobileError> Get()
        {
           // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            List<HistoricoMobileError> historicosList = new List<HistoricoMobileError>();
            historicosList = Context.HistoricoMobileError.Find(new BsonDocument()).ToList();
            List<HistoricoMobileError> SortedLis2t = historicosList.OrderBy(o => o.Id).ToList();
            historicosList = SortedLis2t;
            HistoricoMobileError[] historicos = historicosList.ToArray();
            return historicos;
        }

        // GET: api/HistoricoMobile/data
        public HistoricoMobileError Get(String data)
        {
           // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Expression<Func<HistoricoMobileError, bool>> filter = x => x.Id == data+ "_Error";
            HistoricoMobileError historico = Context.HistoricoMobileError.Find(filter).Single();
            return historico;
        }

        // POST: api/HistoricoMobile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/HistoricoMobile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HistoricoMobile/5
        public void Delete(int id)
        {
        }
    }
}
