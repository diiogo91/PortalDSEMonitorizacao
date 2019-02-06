using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class HistoricoMobile
    {
        public HistoricoMobile(String id, DateTime dataLog, List<HistoricoTransacaoMobile> HistoricoTransacoes)
        {
            this.Id = id;
            this.dataLog = dataLog;
            this.HistoricoTransacoes = HistoricoTransacoes;
        }

        public HistoricoMobile()
        {
            
            
        }
        public String Id { set; get;}
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime dataLog { set; get; }
        public List<HistoricoTransacaoMobile> HistoricoTransacoes { set; get;}
    }
}