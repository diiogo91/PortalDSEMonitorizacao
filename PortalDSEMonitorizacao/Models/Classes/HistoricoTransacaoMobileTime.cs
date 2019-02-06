using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class HistoricoTransacaoMobileTime
    {
        public HistoricoTransacaoMobileTime(ObjectId id, String nome, long tempoTrans, bool found)
        {
            this.Id = id;
            this.Nome = nome;
            this.tempoTrans = tempoTrans;
            this.found = found;
        }

        public HistoricoTransacaoMobileTime()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public String Nome { set; get; }
        public long tempoTrans { set; get; }
        public bool found { set; get; }
    }
}