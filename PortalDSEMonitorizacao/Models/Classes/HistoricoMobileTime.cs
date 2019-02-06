using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class HistoricoMobileTime
    {
        public HistoricoMobileTime(String id, DateTime dataLog, List<HistoricoTransacaoMobileTime> HistoricoTransacoes)
        {
            this.Id = id;
            this.dataLog = dataLog;
            this.HistoricoTransacoes = HistoricoTransacoes;
        }

        public HistoricoMobileTime()
        {
            
            
        }
        public String Id { set; get;}
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime dataLog { set; get; }
        public List<HistoricoTransacaoMobileTime> HistoricoTransacoes { set; get;}
    }
}