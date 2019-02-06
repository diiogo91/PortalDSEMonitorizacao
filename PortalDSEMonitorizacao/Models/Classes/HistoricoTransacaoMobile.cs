using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class HistoricoTransacaoMobile
    {
        public HistoricoTransacaoMobile(ObjectId id, String nome, long nrTransacoes)
        {
            this.Id = id;
            this.Nome = nome;
            this.nrTransacoes = nrTransacoes;
        }

        public HistoricoTransacaoMobile()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public String Nome { set; get; }
        public long nrTransacoes { set; get; }
    }
}