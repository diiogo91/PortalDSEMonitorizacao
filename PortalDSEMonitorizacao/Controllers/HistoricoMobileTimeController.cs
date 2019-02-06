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
    public class HistoricoMobileTimeController : ApiController
    {
        private static PortalDSEMonDB Context = new PortalDSEMonDB();

        // GET: api/HistoricoMobileTime
       
        public IEnumerable<HistoricoMobileTime> Get()
        {
           // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            List<HistoricoMobileTime> historicosList = new List<HistoricoMobileTime>();
            historicosList = Context.HistoricoMobileTime.Find(new BsonDocument()).ToList();
            List<HistoricoMobileTime> SortedLis2t = historicosList.OrderBy(o => o.Id).ToList();
            historicosList = SortedLis2t;
            HistoricoMobileTime[] historicos = historicosList.ToArray();
            return historicos;
        }

        // GET: api/HistoricoMobileTime/data
        public HistoricoMobileTime Get(String data)
        {
           // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Expression<Func<HistoricoMobileTime, bool>> filter = x => x.Id == data+ "_Time";
            HistoricoMobileTime historico = Context.HistoricoMobileTime.Find(filter).Single();
            return historico;
        }

        // POST: api/HistoricoMobileTime
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/HistoricoMobileTime/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HistoricoMobileTime/5
        public void Delete(int id)
        {
        }
    }
}
